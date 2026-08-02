using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
using System.ComponentModel.DataAnnotations;
using Services.Helpers;
using ServiceContracts.Enums;


namespace Services
{
    public class PersonService : IPersonService
    {
        private readonly List<Person> _person;
        private readonly ICountriesService _countryService;

        public PersonService()
        {
            _person = new List<Person>();
            _countryService = new CountriesService();
        }

        //method for converting Person to PersonResponse with country
        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countryService.GetCountryById(person.CountryId)?.CountryName;

            return personResponse;
        }

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {

            //------validation model -- moved to HElPERS folder.-------
            //created a folder /Service/Helpers/ValidationHelpers.cs
            ValidationHelper.ModelValidation(personAddRequest);

            // =============================dont need because moved validation model=======================
            //test 1 -- check if PersonAddrequest is not null
            //if(personAddRequest == null)
            // {
            //     throw new ArgumentNullException(nameof(personAddRequest));
            // }

            ////validate PersonName
            //if(string.IsNullOrEmpty(personAddRequest.PersonName))
            // {
            //     throw new ArgumentException("PersonName cant be blank");
            // };
            // //validate Email
            // if (string.IsNullOrEmpty(personAddRequest.Email))
            // {
            //     throw new ArgumentException("Email cant be blank");
            // }
            // ;
            // =============================dont need because moved validation model=======================

            //Convert personAddRequest from PersonAddRequest type to Person
            Person person =  personAddRequest.ToPerson();

            //generate PersonID
           person.PersonId =  Guid.NewGuid();

            //add person object to person list
            _person.Add(person);

            //convert the Person objet into PersonResponse type

          return ConvertPersonToPersonResponse(person);


        }

        public List<PersonResponse> GetAllPersons()
        {
            return _person.Select(temp => temp.ToPersonResponse()).ToList();  //receives person obje then converts it to personResponseType
            //excecutes once for each person => turns it into PersonResponse then second and third etc. 
            //return I<Enumberable> of person response ---- convert it toLIST();
        }
        public PersonResponse? GetPersonByPersonId(Guid? personId)
        {
            if (personId == null) return null;

          Person? person=_person.FirstOrDefault(temp => temp.PersonId == personId);//will excecute this for each person obj 
            if(person == null) return null;

          return person.ToPersonResponse();
        }

        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
        {
            //check if serahcBy is not null
            List<PersonResponse> allPersons = GetAllPersons();

            List<PersonResponse> matchingPersons = allPersons;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
                return matchingPersons;

            switch (searchBy)
            {
                case nameof(Person.PersonName):
                        matchingPersons = allPersons.Where(temp => 
                        (!string.IsNullOrEmpty(temp.PersonName)? 
                        temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase): true)).ToList();
                    break;

                case nameof(Person.Email):
                    matchingPersons = allPersons.Where(temp =>
                   (!string.IsNullOrEmpty(temp.Email) ? 
                    temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase): true)).ToList();

                    break;


                case nameof(Person.DateOfBirth):
                    matchingPersons = allPersons.Where(temp =>
                    (temp.DateOfBirth != null) ?
                    temp.DateOfBirth.Value.ToString("dd MMMM, yyyy").Contains
                    (searchString, StringComparison.OrdinalIgnoreCase):
                    true).ToList();

                    break;

                case nameof(Person.Gender):
                    matchingPersons = allPersons.Where(temp => 
                    (!string.IsNullOrEmpty(temp.Gender) ?
                    temp.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;

                case nameof(Person.CountryId):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Country) ?
                    temp.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;

                case nameof(Person.Address):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Address) ?
                    temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;

                default: matchingPersons = allPersons; break;
            }
            return matchingPersons;
        }

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
                return allPersons;

            //ascending and descending order for all model types
            List<PersonResponse> sortedPersons = (sortBy, sortOrder) switch
            {
                //name
                (nameof(PersonResponse.PersonName), SortOrderOptions.ASC) =>
                allPersons.OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.PersonName), SortOrderOptions.DESC) =>
              allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                //email 
                (nameof(PersonResponse.Email), SortOrderOptions.ASC) =>
              allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.DESC) =>
                allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                //DOB
                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASC) =>
          allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESC) =>
                allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),

                //age
                (nameof(PersonResponse.Age), SortOrderOptions.ASC) =>
                  allPersons.OrderBy(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.DESC) =>
                allPersons.OrderByDescending(temp => temp.Age).ToList(),

                // GENDER

                (nameof(PersonResponse.Gender), SortOrderOptions.ASC) =>
                  allPersons.OrderBy(temp => temp.Gender).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.DESC) =>
                allPersons.OrderByDescending(temp => temp.Gender).ToList(),

                //Country
                //age
                (nameof(PersonResponse.Country), SortOrderOptions.ASC) =>
                  allPersons.OrderBy(temp => temp.Country).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.DESC) =>
                allPersons.OrderByDescending(temp => temp.Country).ToList(),

                //ADDRESS
                //age
                (nameof(PersonResponse.Address), SortOrderOptions.ASC) =>
                  allPersons.OrderBy(temp => temp.Address).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.DESC) =>
                allPersons.OrderByDescending(temp => temp.Address).ToList(),

                //recieveNewsletters
                //age
                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.ASC) =>
                  allPersons.OrderBy(temp => temp.ReceiveNewsLetters).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.DESC) =>
                allPersons.OrderByDescending(temp => temp.ReceiveNewsLetters).ToList(),

                //defaul
                _ => allPersons //underscore means default
            };

            return sortedPersons;
        }

        public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest == null)
                throw new ArgumentNullException(nameof(Person)); //name of param thats null

            //data anotation as validations for models //call model validation helper
            ValidationHelper.ModelValidation(personUpdateRequest);// throws any errors

            // get matching person objecto to update
          Person? matchingPerson =  _person.FirstOrDefault(temp => temp.PersonId == personUpdateRequest.PersonId);
            if(matchingPerson == null)
            {
                throw new ArgumentException("Given person id doesn't exist");
            }

            //update all details
            matchingPerson.PersonName = personUpdateRequest.PersonName;
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();
            matchingPerson.Address = personUpdateRequest.Address;
            matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
            matchingPerson.CountryId = personUpdateRequest.CountryId;
            matchingPerson.Email = personUpdateRequest.Email;
            matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

            return matchingPerson.ToPersonResponse();
 
        }

        public bool DeletePerson(Guid? personId)
        {
            if(personId == null)
            {
                throw new ArgumentNullException(nameof(personId));
            }
            Person? person = _person.FirstOrDefault(temp => temp.PersonId == personId);
            if (person == null)
                return false;

            _person.RemoveAll(temp => temp.PersonId == personId);

            return true;
        }
    }






}

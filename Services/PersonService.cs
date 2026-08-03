using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;


namespace Services
{
    public class PersonService : IPersonService
    {
        private readonly List<Person> _person;
        private readonly ICountriesService _countryService;

        public PersonService(bool initialize = true)
        {
            _person = new List<Person>();
            _countryService = new CountriesService();

            if (initialize)
            {
                _person.Add(new Person
                {
                    PersonId = Guid.Parse("C87E44E2-8056-4FA1-A4A7-5BDB94B90282"),
                    PersonName = "Mathilde",
                    Email = "mstansfield0@state.tx.us",
                    DateOfBirth = new DateTime(2000, 1, 28),
                    Gender = "Female",
                    Address = "8 Blackbird Way",
                    ReceiveNewsLetters = false,
                    CountryId = Guid.Parse("B50075E5-A2EE-4164-96FD-9C46AEC6CC97") // USA
                });

                _person.Add(new Person
                {
                    PersonId = Guid.Parse("6EAD208A-BF76-4C14-8B91-25CC4F6646F6"),
                    PersonName = "Devondra",
                    Email = "dtills1@php.net",
                    DateOfBirth = new DateTime(1997, 7, 28),
                    Gender = "Female",
                    Address = "67 Ohio Crossing",
                    ReceiveNewsLetters = false,
                    CountryId = Guid.Parse("320217E3-AAC2-40AF-96E0-5522447C6471") // Japan
                });

                _person.Add(new Person
                {
                    PersonId = Guid.Parse("CF68C1EA-40D1-4F09-B9E5-8664D7AC202E"),
                    PersonName = "Skipp",
                    Email = "sklyner2@1688.com",
                    DateOfBirth = new DateTime(1999, 5, 26),
                    Gender = "Male",
                    Address = "29 Maryland Crossing",
                    ReceiveNewsLetters = false,
                    CountryId = Guid.Parse("30C062BE-7371-4F33-B448-D5B40C188100") // Mexico
                });

                _person.Add(new Person
                {
                    PersonId = Guid.Parse("E186EA97-E1EC-4633-B913-70C0E590F724"),
                    PersonName = "Christy",
                    Email = "cchilcotte3@is.gd",
                    DateOfBirth = new DateTime(1997, 12, 13),
                    Gender = "Female",
                    Address = "89 Golf View Pass",
                    ReceiveNewsLetters = false,
                    CountryId = Guid.Parse("A4BF3178-250B-4E46-9DE6-D2B6F27AA72C") // Canada
                });

                _person.Add(new Person
                {
                    PersonId = Guid.Parse("A465DFB6-679C-491C-AFD6-8218544265F2"),
                    PersonName = "Gloriane",
                    Email = "glittrik4@dyndns.org",
                    DateOfBirth = new DateTime(1991, 7, 28),
                    Gender = "Female",
                    Address = "332 Lakewood Hill",
                    ReceiveNewsLetters = true,
                    CountryId = Guid.Parse("449882A6-381F-4630-B17B-BFC070EFE74A") // England
                });

                _person.Add(new Person
                {
                    PersonId = Guid.Parse("55A4A4AA-EAC7-42F7-8B42-3B3E0C70E501"),
                    PersonName = "Daniela",
                    Email = "dpadkin5@sakura.ne.jp",
                    DateOfBirth = new DateTime(1995, 10, 31),
                    Gender = "Female",
                    Address = "74996 Twin Pines Trail",
                    ReceiveNewsLetters = true,
                    CountryId = Guid.Parse("B50075E5-A2EE-4164-96FD-9C46AEC6CC97") // USA
                });

                _person.Add(new Person
                {
                    PersonId = Guid.Parse("3D82B42F-8C8B-43C2-9A42-3F4A6A0F6502"),
                    PersonName = "Shaughn",
                    Email = "sjosifovic6@wordpress.org",
                    DateOfBirth = new DateTime(1991, 3, 30),
                    Gender = "Male",
                    Address = "116 Carey Drive",
                    ReceiveNewsLetters = false,
                    CountryId = Guid.Parse("320217E3-AAC2-40AF-96E0-5522447C6471") // Japan
                });

                _person.Add(new Person
                {
                    PersonId = Guid.Parse("8A36B4C5-3A77-49D5-B4A0-51A4D3F34E03"),
                    PersonName = "Duncan",
                    Email = "dbattleson7@cpanel.net",
                    DateOfBirth = new DateTime(1997, 8, 12),
                    Gender = "Male",
                    Address = "628 Heffernan Crossing",
                    ReceiveNewsLetters = false,
                    CountryId = Guid.Parse("30C062BE-7371-4F33-B448-D5B40C188100") // Mexico
                });

                _person.Add(new Person
                {
                    PersonId = Guid.Parse("F2A77E0D-9A0B-4E0D-98E0-1E5F2C0D8A04"),
                    PersonName = "Shanta",
                    Email = "sbiddiss8@google.com.br",
                    DateOfBirth = new DateTime(1994, 1, 23),
                    Gender = "Female",
                    Address = "813 Swallow Way",
                    ReceiveNewsLetters = true,
                    CountryId = Guid.Parse("A4BF3178-250B-4E46-9DE6-D2B6F27AA72C") // Canada
                });

                _person.Add(new Person
                {
                    PersonId = Guid.Parse("D1C7A0E8-75A5-4A0D-84D6-4A2A1E0A4B05"),
                    PersonName = "Blondy",
                    Email = "bcrippen9@wired.com",
                    DateOfBirth = new DateTime(2000, 4, 30),
                    Gender = "Female",
                    Address = "1 Lakewood Gardens Trail",
                    ReceiveNewsLetters = false,
                    CountryId = Guid.Parse("449882A6-381F-4630-B17B-BFC070EFE74A") // England
                });
            }
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

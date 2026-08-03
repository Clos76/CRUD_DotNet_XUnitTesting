using ServiceContracts;
using System;
using System.Collections.Generic;
using Xunit;
using ServiceContracts;
using Entities;
using ServiceContracts.DTO;
using Services;
using System.Xml.Serialization;
using ServiceContracts.Enums;
using System.Globalization;
using Xunit.Abstractions;

namespace CRUDTests
{
    public class PersonServiceTest

    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IPersonService _personService;
        private readonly ICountriesService _countriesService;

        //constructor
        public PersonServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personService = new PersonService(); //everytime it runs it , it creates a new instance of PersonServiceTest;
            _countriesService = new CountriesService(false);
            _testOutputHelper = testOutputHelper; //test helper shows what supposed value should be, expected and actual value
        }

        #region AddPerson
        [Fact]
        //when we supply null value as PersonAddRequest, it should throw argumentnull exce
        public void AddPerson_NullPerson()
        {
            //ARRANGE - create the input
            PersonAddRequest personAddRequest = null;
            //ACT
            Assert.Throws<ArgumentNullException>(() =>
            {
                _personService.AddPerson(personAddRequest); //execute the method were doing
            });
            //ASSERT
        }

        [Fact]
        //when we supply null value as PersonAddRequest, it should throw ArgumentException
        public void AddPerson_PersonNameIsNull()
        {
            //ARRANGE - create the input
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = null,

            };
            //ACT
            Assert.Throws<ArgumentException>(() =>
            {
                _personService.AddPerson(personAddRequest); //execute the method were doing
            });
            //ASSERT
        }
        [Fact]
        //when we supply null value as PersonAddRequest, it should throw argumentnull exce
        public void AddPerson_EmailIsNull()
        {
            //ARRANGE - create the input
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                Email = null
            };
            //ACT
            Assert.Throws<ArgumentException>(() =>
            {
                _personService.AddPerson(personAddRequest); //execute the method were doing
            });
            //ASSERT
        }

        [Fact]
        //when we supply proper person details, it should insert the person inside the person list, 
        //should return an obj with PersonResponse with newly generated id
        public void AddPerson_ProperPersonDetails()
        {
            //ARRANGE - create the input
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = "John",
                Email = "person@gmail.com",
                Address = "sample Address",
                CountryId = Guid.NewGuid(),
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true,

            };
            //ACT
            PersonResponse person_response_from_add = _personService.AddPerson(personAddRequest);
            List<PersonResponse> persons_list = _personService.GetAllPersons();

            //ASSERT
            Assert.True(person_response_from_add.PersonId != Guid.Empty);
            Assert.Contains(person_response_from_add, persons_list);

        }



        //ARRANGE
        //ACT
        //ASSERT

        #endregion

        #region GetPersonPersonId

        //if we supply null as PersonId , it should return null as PersonResponse
        [Fact]
        public void GetPersonByPersonId_NullPersonId()
        {
            //arange
            Guid? personId = null;

            //act
            PersonResponse? person_response_from_get = _personService.GetPersonByPersonId(personId);

            //ASSERT
            Assert.Null(person_response_from_get);


        }

        [Fact]
        //if we supply a valid person id, it should return the valid person details as PersonResponse object
        public void GetPersonByPersonId_WithPersonId()
        {
            //Arrange
            CountryAddRequest country_request = new CountryAddRequest() { CountryName = "Canada" };
            CountryResponse countryReponse = _countriesService.AddCountry(country_request);

            //ACT
            PersonAddRequest person_request = new PersonAddRequest()
            {
                PersonName = "John",
                Address = "123 john way",
                Email = "mail@gmail.com",
                CountryId = countryReponse.CountryId,
                DateOfBirth = DateTime.Parse("2001-03-23"),
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = false
            };

            //varification
            //here were adding the new person
            PersonResponse person_response_from_add = _personService.AddPerson(person_request);

            //here we should be getting back the same person added ontop - to indicate added successfully
            PersonResponse? person_response_from_get =
            _personService.GetPersonByPersonId(person_response_from_add.PersonId);

            //ASSERT
            Assert.Equal(person_response_from_add, person_response_from_get);
        }

        #endregion

        #region GetAllPersons
        //The GetAllPersons() should return an empty list by default
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            //ACT
            List<PersonResponse> person_from_get = _personService.GetAllPersons();

            //ASSERT 
            Assert.Empty(person_from_get); //direct method -- expect list to be empty

        }

        //call more than one person, call all people
        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            //Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "INdia" };

            CountryResponse country_to_add1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_to_add2 = _countriesService.AddCountry(country_request_2);


            PersonAddRequest person_add = new PersonAddRequest()
            {
                PersonName = "Jone",
                ReceiveNewsLetters = true,
                Address = "123 way ",
                Email = "panchito@gmail.com",
                DateOfBirth = DateTime.Parse("2000-01-01"),
                CountryId = country_to_add1.CountryId,
                Gender = GenderOptions.Female,
            };
            PersonAddRequest person_add2 = new PersonAddRequest()
            {
                PersonName = "Jone",
                ReceiveNewsLetters = true,
                Address = "123 way ",
                Email = "pan@gmail.com",
                DateOfBirth = DateTime.Parse("2500-01-01"),
                CountryId = country_to_add2.CountryId,
                Gender = GenderOptions.Female,
            };
            PersonAddRequest person_add3 = new PersonAddRequest()
            {
                PersonName = "Jone",
                ReceiveNewsLetters = true,
                Address = "123 way ",
                Email = "hito@gmail.com",
                DateOfBirth = DateTime.Parse("2005-05-01"),
                CountryId = country_to_add1.CountryId,
                Gender = GenderOptions.Female,
            };

            List<PersonAddRequest> persons = new List<PersonAddRequest>()
            { person_add, person_add2, person_add3 };

            //create empty list to contain the foreach 
            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person in persons)
            {
                PersonResponse person_resonse = _personService.AddPerson(person);

                person_response_list_from_add.Add(person_resonse);
            }


            //print person_response_list_from_add -in test 
            _testOutputHelper.WriteLine("Expected:");

            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
            ///ACT
            ///get all persons now
            List<PersonResponse> person_list_from_get = _personService.GetAllPersons();

            //print ACTUAl Data -in test 
            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_reponse_from_get in person_list_from_get)
            {
                _testOutputHelper.WriteLine
                    (person_reponse_from_get.ToString());
            }

            //ASSERT 
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                Assert.Contains(person_response_from_add, person_list_from_get);
            }

        }

        #endregion


        #region GetSortedPersons
        //when we sort based on personName in DESC it should return persons list in desc order
        [Fact]
        public void GetFilteredPersons_SearchByPersonName()
        {
            //Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "INdia" };

            CountryResponse country_to_add1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_to_add2 = _countriesService.AddCountry(country_request_2);


            PersonAddRequest person_add = new PersonAddRequest()
            {
                PersonName = "Jesseane",
                ReceiveNewsLetters = true,
                Address = "123 way ",
                Email = "panchito@gmail.com",
                DateOfBirth = DateTime.Parse("2000-01-01"),
                CountryId = country_to_add1.CountryId,
                Gender = GenderOptions.Female,
            };
            PersonAddRequest person_add2 = new PersonAddRequest()
            {
                PersonName = "Joe",
                ReceiveNewsLetters = true,
                Address = "123 way ",
                Email = "pan@gmail.com",
                DateOfBirth = DateTime.Parse("2500-01-01"),
                CountryId = country_to_add2.CountryId,
                Gender = GenderOptions.Female,
            };
            PersonAddRequest person_add3 = new PersonAddRequest()
            {
                PersonName = "Jane",
                ReceiveNewsLetters = true,
                Address = "123 way ",
                Email = "hito@gmail.com",
                DateOfBirth = DateTime.Parse("2005-05-01"),
                CountryId = country_to_add1.CountryId,
                Gender = GenderOptions.Female,
            };


            //adding the 3 objects into a collection persons
            List<PersonAddRequest> person_request = new List<PersonAddRequest>()
            { person_add, person_add2, person_add3 };

            //create empty list to contain the foreach for PersonResponse with personID --- 
            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();
            //add each obj from request into persons - with id
            foreach (PersonAddRequest person in person_request)
            {
                PersonResponse person_resonse = _personService.AddPerson(person);

                person_response_list_from_add.Add(person_resonse);
            }


            //print person_response_list_from_add -in test 
            _testOutputHelper.WriteLine("Expected:");

            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }


            //get all persons
            List<PersonResponse> allPersons = _personService.GetAllPersons();

            ///ACT
            ///get all persons sorted -returns order descending 
            List<PersonResponse> persons_list_from_sort = _personService.GetSortedPersons(allPersons, nameof(Person.PersonName), SortOrderOptions.DESC );

            //print ACTUAl Data -in test 
            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_reponse_from_get in persons_list_from_sort)
            {
                _testOutputHelper.WriteLine
                    (person_reponse_from_get.ToString());
            }

            //sort person obj //order by descending
           person_response_list_from_add =  person_response_list_from_add.OrderByDescending(temp => temp.PersonName).ToList();

            //ASSERT 
            //now we compare them - first obj in expected list and actual list, each property should be equal
           for(int i=0; i<person_response_list_from_add.Count; i++)
            {
                Assert.Equal(person_response_list_from_add[i], persons_list_from_sort[i]); ///2 collections here. 1st one is the expected. 2nd is the actual
            }

        }
        #endregion


        #region UpdatePerson
        //when we supply null as PersonUpdateRequest, it should thorw ArgumentNullExcepction
        [Fact]
        public void UpdatePerson_NullPerson()
        {
            //ARRANGE
            PersonUpdateRequest? person_update_request = null; //create a null obj

            //ASSERT
            Assert.Throws<ArgumentNullException>(() => //expected type // keep method call taht makes exception inside the lambda
            {
                //ACT
                _personService.UpdatePerson(person_update_request); //call the update obj and supply the var from null above
            });
        }


        //case personId is invalid 
        [Fact]
        public void UpdatePerson_InvalidPersonId()
        {
            //ARRANGE
            PersonUpdateRequest? person_update_request = new
                PersonUpdateRequest() { PersonId = Guid.NewGuid() };

            //ASSERT

            Assert.Throws<ArgumentException>(() =>
            {//ACT
                _personService.UpdatePerson(person_update_request);
            });

        }

        [Fact]
        //when person name null , should throw argumentException
        public void UpdatePerson_PersonNameNull()
        {

            //ARRANGE
            //create a new person--first create country because dependatn
            CountryAddRequest? country_add_request = new CountryAddRequest() { CountryName = "UK" };
            //add country to countryService
            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            //create person obj with countryId from above
            PersonAddRequest person_add_request = new PersonAddRequest()
            {
                PersonName = "John",
                CountryId = country_response_from_add.CountryId,
                Email = "joyh@hom.com",
                Gender = GenderOptions.Male
            };
            //Call addPersonService for this, personAddReuqest, adds and returns as personsRepsonse
            //now have personID
           PersonResponse person_response_from_add =  _personService.AddPerson(person_add_request);

            // create person now with country id
            PersonUpdateRequest person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = null;

            //ASSSERT
            Assert.Throws<ArgumentException>(() =>
            {
                //ACT
                _personService.UpdatePerson(person_update_request);
            });

        }

        //
        [Fact]
        //Add new person and try to update it, person name and email
        public void UpdatePerson_PersonFullDetailsUpdate()
        {

            //ARRANGE
            //create a new person--first create country because dependatn
            CountryAddRequest? country_add_request = new CountryAddRequest() { CountryName = "UK" };
            //add country to countryService
            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            //create person obj with countryId from above
            PersonAddRequest person_add_request = new PersonAddRequest()
            {
                PersonName = "John",
                CountryId = country_response_from_add.CountryId,
                Address = "123 adress",
                DateOfBirth = DateTime.Parse("2001-01-12"),
                Email = "Mail@Mail.com",
                Gender = GenderOptions.Female,
                ReceiveNewsLetters = false

            };
            //Call addPersonService for this, personAddReuqest, adds and returns as personsRepsonse
            //now have personID
            PersonResponse person_response_from_add = _personService.AddPerson(person_add_request);

            // create person now with country id
            PersonUpdateRequest person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName ="William";
            person_update_request.Email = "Will@will.com";

            //ACT
          PersonResponse person_response_from_update = _personService.UpdatePerson(person_update_request);

            //check if theyre equal
         PersonResponse?  person_response_from_get =    _personService.GetPersonByPersonId(person_response_from_update.PersonId);


            //ASSSERT
            Assert.Equal( person_response_from_get, person_response_from_update); // updated and checking against the actual
        }


        #endregion


        #region DeletePerson
        //if you supply an a valid personId , it should return true
        [Fact]
        public void DeletePerson_validPersonId()
        {
            CountryAddRequest country_add_request = new CountryAddRequest() { CountryName= "Germania" };
          CountryResponse country_response_from_add =   _countriesService.AddCountry(country_add_request);

            PersonAddRequest person_add_request = new PersonAddRequest()
            {
                PersonName = "Milky",
                Email = "Milk@you.com",
                CountryId = country_response_from_add.CountryId,
                Address = "122 adress",
                DateOfBirth = DateTime.Parse("2100-02-02"),
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = false
            };

            PersonResponse person_response_from_add = _personService.AddPerson(person_add_request);

            //ACT
           bool isDeleted = _personService.DeletePerson(person_response_from_add.PersonId);

            //ASSERT
            Assert.True(isDeleted);

        }

        //if you supply an a invalid personId , it should return false
        [Fact]
        public void DeletePerson_InvalidPersonId()
        {




            //ACT
            bool isDeleted = _personService.DeletePerson(Guid.NewGuid()); //newly guid should be false

            //ASSERT
            Assert.False(isDeleted);

        }

        #endregion


    }
}

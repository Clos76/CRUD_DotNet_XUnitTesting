using System;
using System.Collections.Generic;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using Services;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();
        }

        #region AddCountry


        //when countryAddREquest is null, it should ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry()
        {
            //arrange 
            CountryAddRequest? request = null;

            //ASSERT && ACT
            Assert.Throws<ArgumentNullException>(() =>
            {
                _countriesService.AddCountry(request);
            });

        }
        //when the CountryName is null, it should throw argumentException
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            //arrange 
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = null
            };


            //ASSERT
            Assert.Throws<ArgumentException>(() =>
            {
                _countriesService.AddCountry(request);
            });

           
        }
        //when countryName i sduplicate,, it should throw argumentException
        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            //arrange 
            CountryAddRequest? request1 = new CountryAddRequest()
            {
                CountryName = "USA"
            };

            CountryAddRequest request2 = new CountryAddRequest()
            {
                CountryName = "USA"
            };

            //ASSERT
            Assert.Throws<ArgumentException>(() =>
            {  //ACT
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });

          
        }
        //when you supply proper countryname, it should insert (add) the country to the exsiting list of countries
        [Fact]
        public void AddCountry_ProperCountryDetails()
        {
            //arrange 
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = "Japan"
            };

            //ASSERT
            CountryResponse response = _countriesService.AddCountry(request);
            //calling getAllCoutries method-- receiving returned value as countries-from-getAll-countries
           List<CountryResponse> countries_from_GetAllCountries =  _countriesService.GetAllCountries();

            //ACT

            Assert.True(response.CountryId != Guid.Empty);
            //.Contains calls the Equals() method, now compares their values when overide the Equals() funciton
            Assert.Contains(response, countries_from_GetAllCountries); //---ned to verify that the country is there as well
        }
        //need to compare acutally values, right now just compares the reference

        #endregion

        #region GetAllCountries
        [Fact]
        //list of countries should be empty by default
        public void GetAllCountries_EmptyList()
        {
            //ARRANGE
            //ACT
            List<CountryResponse> actual_country_response_list =
            _countriesService.GetAllCountries();

            //ASSERT--test whether the list is empty
            Assert.Empty(actual_country_response_list);

        }

        //test 2 if add countries, it should return those countries
        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            //ARRANGE
            List<CountryAddRequest> country_request_list = new List<CountryAddRequest>()
            {
                new CountryAddRequest() { CountryName ="USA"},
                new CountryAddRequest(){CountryName = "UK"}
            };

            //ACT
            //creates empty list to store the results returned by AddCountry();
            List<CountryResponse> countries_list_from_add_country = new List<CountryResponse>();

            foreach(CountryAddRequest country_request in country_request_list)
            {
              countries_list_from_add_country.Add(_countriesService.AddCountry(country_request));
            }

            //calls the getAll from countryRespose
           List<CountryResponse> actualCountryResponseList =  _countriesService.GetAllCountries();

            //read each element from countries_list_from_add_country
            foreach(CountryResponse expected_country in countries_list_from_add_country)
            {
                Assert.Contains(expected_country, actualCountryResponseList);
            }
        }


        #endregion


        #region GetCountryByCountryId

        //if supply null 
        [Fact]
        public void GetCountryByCountryId_NullCountryId()
        {
            //ARRANGE
            Guid? countryId = null;

            //ACT
         CountryResponse? country_response_from_get_method = _countriesService.GetCountryById(countryId);

            //ASSERT
            Assert.Null(country_response_from_get_method);

        }

        //if supply valid country id, it should return matching country details as countryREsponse object
        [Fact]
        public void GetCountryById_ValidCountryId()
        {
            //ARRANGE
            //always runs independantly, other tests,will not be considered. everyUnit test, country list be empty
          CountryAddRequest? country_add_request = new CountryAddRequest()
               { CountryName = "USA" };
           
         CountryResponse country_response_from_add =  _countriesService.AddCountry(country_add_request);

            //ACT
            //call getCountryById method
          CountryResponse? country_response_from_get=  _countriesService.GetCountryById
                (country_response_from_add.CountryId);


            //compare from add and get from both above
            //ASSERT
            Assert.Equal(country_response_from_add, country_response_from_get);
        }
        #endregion
    }
}
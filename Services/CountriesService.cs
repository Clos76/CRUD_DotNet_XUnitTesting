using ServiceContracts;
using ServiceContracts.DTO;
using Entities;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private readonly List<Country> _countries;
        //constructor
        public CountriesService()
        {
            //saying whenever countries service is created , create a brand new empty list of countries.
            _countries = new List<Country>(); //inject from /Entities/Country-------
        }
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            // Check if "countryAddRequest" is not null
            // Validate all properties of "countryAddRequest"
            // Convert "countryAddRequest" from "CountryAddRequest" type to "Country"
            //Generate a new CountryID
            //Then add it into List<Country>
            // Return CountryResponse object with generated CountryID

            //VALIDATION: countryAddRequest paramater can't be null -----
            if(countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }

            //VALIDATION: If countryName is null
            if(string.IsNullOrWhiteSpace(countryAddRequest.CountryName))
            {
                throw new ArgumentException("Country name can't be blank", nameof(countryAddRequest.CountryName));
            };

            //VALIDATION: CountryName can't be duplicate
            if(_countries.Where(temp => 
            temp.CountryName == countryAddRequest.CountryName).Count() > 0)
            {
                throw new ArgumentException("Given country name already exists");
            }

            //convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry(); //goes to /ServiceContracts/DTO/CountryAddRequest-------
            //genterate CountryID 
            country.CountryId = Guid.NewGuid(); //goes to /ServiceContracts/DTO/CountryAddRequest-------


            //Add country object into _countries
            _countries.Add(country);

            return country.ToCountryResponse(); //goes to /ServiceContracts/DTO/CountryResponse.cs-------
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(c => c.ToCountryResponse()).ToList() ;
        }

        public CountryResponse GetCountryById(Guid? countryId)
        {
          if (countryId == null)
            {
                return null;
            }
         Country? country_response_from_list =    _countries.FirstOrDefault(temp => temp.CountryId == countryId);

            if(country_response_from_list == null)
            {
                return null;
            }
            return country_response_from_list.ToCountryResponse();
          
        }
    }
}

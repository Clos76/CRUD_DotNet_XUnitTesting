using ServiceContracts.DTO;


namespace ServiceContracts
{   
    /// <summary>
    /// Represents business logic for manipulationg Country entity
    /// </summary>

    public interface ICountriesService
    {
        /// <summary>
        /// Adds a country object to the list of Countries
        /// </summary>
        /// <param name="countryAddRequest">Country to add </param>
        /// <returns>returns the country object after adding it (including
        /// newly genertaed country id </returns>
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

       /// <summary>
       /// Returns all countries from the list
       /// </summary>
       /// <returns>All countries from the list as List of CountryResponse</returns>
        List<CountryResponse> GetAllCountries();


        /// <summary>
        /// Returns country object based in given country id
        /// </summary>
        /// <param name="countryId"> CountryId (guid) to search</param>
        /// <returns>Matching Country as CountryResponse object</returns>
        CountryResponse? GetCountryById(Guid? countryId);
       
    }
}

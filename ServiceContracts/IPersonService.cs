using System;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ServiceContracts
{
   public interface IPersonService
    {
        /// <summary>
        /// Adds a new person into the list of persons
        /// </summary>
        /// <param name="personAddRequest">Person to add</param>
        /// <returns>Returns the same person details, along with newly generated PersonId</returns>
      PersonResponse AddPerson(PersonAddRequest? personAddRequest);

        /// <summary>
        /// Returns all persons from db or table
        /// </summary>
        /// <returns> Returns list of object of PersonResponse type</returns>
        List<PersonResponse> GetAllPersons();

        /// <summary>
        /// Returns the person object based on the give person id
        /// </summary>
        /// <param name="personId">Person id to search</param>
        /// <returns>Returns matching person object</returns>
      PersonResponse? GetPersonByPersonId(Guid? personId);

        /// <summary>
        /// Returns all person object that matche withthe given search field and search string
        /// </summary>
        /// <param name="searchBy">Search field to search</param>
        /// <param name="searchString">Search string to search</param>
        /// <returns>Returns all matrching persons based on the given search field and string </returns>
      List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString);

        /// <summary>
        /// Returns sorted list of persons
        /// </summary>
        /// <param name="allPersons">Represents list of persons to sort</param>
        /// <param name="sortBy">Name of property (key) based on which the persons should be sorted</param>
        /// <param name="sortOrder">ASC or DESC</param>
        /// <returns>Returns osrtd persons as PersonResponse list</returns>
        List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);


        /// <summary>
        /// Updates the specified person details based on the given person ID
        /// </summary>
        /// <param name="personUpdateRequest">Person details to update, including person id</param>
        /// <returns>Returns the person response object after updating</returns>
       PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest); //method-- returns a PersonResponse obj

        /// <summary>
        /// Deletes a peson based on the give person id
        /// </summary>
        /// <param name="personId">PersonId to delete</param>
        /// <returns>Returns true, if the delation is succesful; otherwise false</returns>
       bool DeletePerson(Guid? personId);
    }
}

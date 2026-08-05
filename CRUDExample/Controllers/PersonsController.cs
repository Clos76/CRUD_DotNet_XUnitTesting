using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace CRUDExample.Controllers
{
    public class PersonsController : Controller
    {

        //private
        private readonly IPersonService _personService;
        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }
        [Route("persons/index")]
        [Route("/")]
        public IActionResult Index(string searchBy, string? searchString, 
            string sortBy = nameof(PersonResponse.PersonName),
            SortOrderOptions sortOrder = SortOrderOptions.ASC)//model binding
        {
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                { nameof(PersonResponse.PersonName), "Person Name" },
                {nameof(PersonResponse.Email), "Email"},
                {nameof(PersonResponse.Address), "Address"},
                {nameof(PersonResponse.Gender), "Gender"},
                {nameof(PersonResponse.CountryId), "Country ID" }

            };

            List<PersonResponse> persons = _personService.GetFilteredPersons(searchBy, searchString);
            //store the searchBy and searchString into a var to keep it in the view
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            //sorting code
            List<PersonResponse> sortedPersons = _personService.GetSortedPersons(persons, sortBy, sortOrder);//matching persons from GetFilteredPersons above

            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();



            return View(persons); //views/persons/index---- but we also supply the model value data
        }
    }
}

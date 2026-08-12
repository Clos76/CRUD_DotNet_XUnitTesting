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
        private readonly ICountriesService _countriesSerivice;
        public PersonsController(IPersonService personService, ICountriesService countriesService  )
        {
            _personService = personService;
            _countriesSerivice = countriesService;
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



            return View(sortedPersons); //views/persons/index---- but we also supply the model value data
        }

        //Executes when the use cliks on "Create Person" link in the Index view
        [Route("persons/create")]
        [HttpGet] //this is to open the link
        public IActionResult Create()
        {
           List<CountryResponse> countries = _countriesSerivice.GetAllCountries();
            ViewBag.Countries = countries;

            return View();
        }

        [HttpPost]
        [Route("persons/create")]
        public IActionResult Create(PersonAddRequest personAddRequest)
        {
            if (!ModelState.IsValid)
            {
                List<CountryResponse> countries = _countriesSerivice.GetAllCountries();
                ViewBag.Countries = countries;
              ViewBag.Errors =ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return View();
            }

            //no errors
            PersonResponse personResponse = _personService.AddPerson(personAddRequest);

            return RedirectToAction("Index", "Persons"); // redirect to index, then what controller (persons)
        }
    }
}

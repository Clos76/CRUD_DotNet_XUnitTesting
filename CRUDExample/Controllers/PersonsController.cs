using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

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
        public IActionResult Index(string searchBy, string? searchString)//model binding
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

            return View(persons); //views/persons/index---- but we also supply the model value data
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Internship.DAL.Repositories;
using Internship.Models;
namespace Internship.Controllers
{
    public class PersonController : Controller
    {

        private readonly PersonRepository _personRepository;

        // Constructor to initialize the repository
        public PersonController()
        {
            _personRepository = new PersonRepository();
        }
        public IActionResult Index()
        {
            List<Person> persons = _personRepository.GetAllPersons();
            return View(persons);
        }

        // GET: Asset/Person
        public IActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                _personRepository.CreatePerson(person);
                return RedirectToAction(nameof(Index)); // Redirect to the list after creation
            }

            return View(person); // Return to the form with validation errors if any
        }

        public IActionResult Details(int PId)
        {
            // Get the asset by AssetId from the repository
            Person person = _personRepository.GetPersonById(PId);

            if (person == null)
            {
                return NotFound($"No asset category found with person ID {PId}");
            }
            return View(person);
        }
    }
}

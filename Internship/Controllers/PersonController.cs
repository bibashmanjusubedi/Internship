using Microsoft.AspNetCore.Mvc;
using Internship.DAL.Repositories;
using Internship.Models;
using Microsoft.AspNetCore.Authorization;
namespace Internship.Controllers
{

    [Authorize(Roles = "Admin")]
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

        // GET: Person/Delete/5
        public IActionResult Delete(int PId)
        {
            var person = _personRepository.GetPersonById(PId);
            if (person == null)
            {
                return NotFound($"No person found with ID {PId}");
            }
            return View(person); // Show confirmation view
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int PId)
        {
            _personRepository.DeletePerson(PId);
            return RedirectToAction(nameof(Index)); // After deletion, return to list
        }


        // GET: Person/Edit/5
        public IActionResult Edit(int PId)
        {
            var person = _personRepository.GetPersonById(PId);
            if (person == null)
            {
                return NotFound($"No person found with ID {PId}");
            }
            return View(person); // Show the edit form with current data
        }

        // POST: Person/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                _personRepository.UpdatePerson(person);
                return RedirectToAction(nameof(Index)); // Redirect to the list after update
            }

            return View(person); // Return to the form with validation errors
        }


    }
}

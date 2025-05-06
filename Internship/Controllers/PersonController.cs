using Microsoft.AspNetCore.Mvc;
using Internship.DAL.Repositories;
using Internship.Models;
using Microsoft.AspNetCore.Authorization;
namespace Internship.Controllers
{

    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly PersonRepository _personRepository;

        // Constructor to initialize the repository
        public PersonController()
        {
            _personRepository = new PersonRepository();
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            List<Person> persons = _personRepository.GetAllPersons();
            return Ok(persons);
        }

        // GET: Asset/Person
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return Ok();
        }

        // POST: Person/Create
        [HttpPost("Create")]
        public IActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                _personRepository.CreatePerson(person);
                return RedirectToAction(nameof(Index)); // Redirect to the list after creation
            }

            return Ok(person); // Return to the form with validation errors if any
        }

        [HttpGet("Details/{PId}")]
        public IActionResult Details(int PId)
        {
            // Get the asset by AssetId from the repository
            Person person = _personRepository.GetPersonById(PId);

            if (person == null)
            {
                return NotFound($"No asset category found with person ID {PId}");
            }
            return Ok(person);
        }

        // GET: Person/Delete/5
        [HttpGet("Delete/{PId}")]
        public IActionResult Delete(int PId)
        {
            var person = _personRepository.GetPersonById(PId);
            if (person == null)
            {
                return NotFound($"No person found with ID {PId}");
            }
            return Ok(person); // Show confirmation view
        }

        // POST: Person/Delete/5
        [HttpDelete("Delete/{PId}")]
        public IActionResult DeleteConfirmed(int PId)
        {
            _personRepository.DeletePerson(PId);
            return RedirectToAction(nameof(Index)); // After deletion, return to list
        }


        // GET: Person/Edit/5
        [HttpGet("Edit/{PId}")]
        public IActionResult Edit(int PId)
        {
            var person = _personRepository.GetPersonById(PId);
            if (person == null)
            {
                return NotFound($"No person found with ID {PId}");
            }
            return Ok(person); // Show the edit form with current data
        }

        // POST: Person/Edit/5
        [HttpPut("Edit/{PId}")]
        public IActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                _personRepository.UpdatePerson(person);
                return RedirectToAction(nameof(Index)); // Redirect to the list after update
            }

            return Ok(person); // Return to the form with validation errors
        }


    }
}

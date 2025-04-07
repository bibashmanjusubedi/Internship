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

        //// GET: Asset/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}
    }
}

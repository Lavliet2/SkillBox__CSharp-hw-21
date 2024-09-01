using Homework_20.Services;
using WebAPIContacts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Homework_20.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContactService _contactService;

        public HomeController(ContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.GetContactsAsync();
            ViewBag.Contacts = contacts;
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.UserName = User.Identity.Name;
            }
            else
            {
                ViewBag.UserName = "Гость";
            }
            return View(contacts);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            await _contactService.AddContactAsync(contact);
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var contacts = await _contactService.GetContactsAsync();
            var contact = contacts.FirstOrDefault(c => c.ID == id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var contacts = await _contactService.GetContactsAsync();
            var contact = contacts.FirstOrDefault(c => c.ID == id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            await _contactService.UpdateContactAsync(contact);
            return RedirectToAction("Details", new { id = contact.ID });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactService.DeleteContactAsync(id);
            return RedirectToAction("Index");
        }
    }
}

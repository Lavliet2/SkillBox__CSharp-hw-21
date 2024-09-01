using Microsoft.AspNetCore.Mvc;
using WebAPIContacts.Iterfaces;
using WebAPIContacts.Models;

namespace WebAPIContacts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactData contactData;

        public ContactsController(IContactData contactData)
        {
            this.contactData = contactData;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contact>> Get()
        {
            var contacts = contactData.GetContacts();
            return Ok(contacts);
        }

        [HttpPost]
        public ActionResult Add([FromBody] Contact contact)
        {
            contactData.AddContact(contact);
            return Ok();
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Contact contact)
        {
            if (contact == null || contact.ID != id)
            {
                return BadRequest();
            }

            var contactToUpdate = contactData.GetContacts().SingleOrDefault(c => c.ID == id);

            if (contactToUpdate == null)
            {
                return NotFound();
            }

            contactData.UpdateContact(contact);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            bool result = contactData.DeleteContact(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}

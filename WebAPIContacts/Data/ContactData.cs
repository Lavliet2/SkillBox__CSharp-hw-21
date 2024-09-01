using WebAPIContacts.Iterfaces;
using WebAPIContacts.DataContext;
using WebAPIContacts.Models;

namespace WebAPIContacts.Data
{
    public class ContactData : IContactData
    {
        private readonly ContactDbContext context;

        public ContactData(ContactDbContext Context)
        {
            this.context = Context;
        }
        public IEnumerable<Contact> GetContacts()
        {
            return this.context.Contacts;
        }

        public void AddContact(Contact contact)
        {
            context.Contacts.Add(contact);
            context.SaveChanges();
        }
        public void UpdateContact(Contact contact)
        {
            var existingContact = context.Contacts.Find(contact.ID);
            if (existingContact != null)
            {
                context.Entry(existingContact).CurrentValues.SetValues(contact);
                context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException("Contact not found.");
            }
        }
        public bool DeleteContact(int id)
        {
            var contact = context.Contacts.Find(id);
            if (contact != null)
            {
                context.Contacts.Remove(contact);
                context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}

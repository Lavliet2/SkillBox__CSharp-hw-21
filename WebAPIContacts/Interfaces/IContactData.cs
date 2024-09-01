using WebAPIContacts.Models;

namespace WebAPIContacts.Iterfaces
{
    public interface IContactData
    {
        IEnumerable<Contact> GetContacts();
        void AddContact(Contact contact);
        void UpdateContact(Contact contact);
        bool DeleteContact(int id);

        //void SaveContact();

    }
}

using Microsoft.EntityFrameworkCore;
using WebAPIContacts.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebAPIContacts.AuthContactApp;


namespace WebAPIContacts.DataContext
{
    public class ContactDbContext : IdentityDbContext<User>
    {
        public ContactDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Contact> Contacts { get; set; }
    }
}

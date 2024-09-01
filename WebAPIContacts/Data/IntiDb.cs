using WebAPIContacts.DataContext;
using WebAPIContacts.Models;


namespace Homework_20.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ContactDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Contacts.Any()) return;

            var sections = new List<Contact>()
            {
                new Contact(){LastName ="Audi", FirstName="Q8", MiddleName="Gorod", Address = "23", PhoneNumber = "123", Description = "Desc" }
            };
            using (var trans = context.Database.BeginTransaction())
            {
                foreach (var section in sections)
                {
                    context.Contacts.Add(section);
                }

                //context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Cars] ON");
                //context.SaveChanges();
                //context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Cars] OFF");
                trans.Commit();
            }


        }
    }
}
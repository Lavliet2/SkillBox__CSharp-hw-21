namespace WebAPIContacts.Models
{
    public class NullContact : IContact
    {
        private NullContact() 
        {
            this.ID = 0;
            this.LastName = "nil";
            this.FirstName = "nil";
            this.MiddleName = "nil";
            this.PhoneNumber = "nil";
            this.Address = "nil";
            this.Description = "nil";            
        }
        static public NullContact Create()
        {
            return new NullContact();
        }
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}

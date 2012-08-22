using System;
using CsvHelper.Configuration;

namespace Contax.Web.UI.Domain
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FacebookUserName { get; set; }
        public string TwitterUserName { get; set; }

    }

    public class ContactCsvMap : CsvClassMap<Contact>
    {
        public ContactCsvMap()
        {
            Map(c => c.FirstName).Name("First Name");
            Map(c => c.LastName).Name("Last Name");
            Map(c => c.FacebookUserName).Name("facebook");
            Map(c => c.TwitterUserName).Name("twitter");
        }
    }
}
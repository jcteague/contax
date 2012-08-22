using System.Collections.Generic;
using System.IO;
using Contax.Web.UI.Domain;
using CsvHelper;
using CsvHelper.Configuration;

namespace Contax.Web.UI.Services
{
    public interface IUploadContacts
    {
        ServiceResult<Contact> Upload(Stream inputStream);
    }

    public class ContactCsvUploader : IUploadContacts
    {
        

        public ServiceResult<Contact> Upload(Stream inputStream)
        {
            
            var cvs_helper = new CsvReader(new StreamReader(inputStream));
            cvs_helper.Configuration.HasHeaderRecord = true;
            cvs_helper.Configuration.IsCaseSensitive = false;
            //cvs_helper.Configuration. = false;
            cvs_helper.Configuration.ClassMapping<ContactCsvMap, Contact>();
            var contacts = cvs_helper.GetRecords<Contact>();
            return new ServiceResult<Contact>()
                       {
                           Success = true,
                           Data = contacts
                       };
        }
    }
}
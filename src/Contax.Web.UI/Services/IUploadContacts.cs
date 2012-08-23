using System.Collections;
using System.IO;
using Contax.Web.UI.Domain;
using System.Linq;

namespace Contax.Web.UI.Services
{
    public interface IUploadContacts
    {
        ServiceResult<Contact> Upload(Stream inputStream);
    }

    public class ContactCsvUploader : IUploadContacts
    {
        IParseFiles _parser;
        readonly IContactRepository _contactRepository;

        public ContactCsvUploader(IParseFiles parser, IContactRepository contactRepository)
        {
            _parser = parser;
            _contactRepository = contactRepository;
        }

        public ServiceResult<Contact> Upload(Stream inputStream)
        {

            var contacts = _parser.Parse<Contact>(inputStream,
                                                  (configuration) =>
                                                  configuration.ClassMapping<Contact, ContactCsvMap>());
            _contactRepository.Save(contacts.ToArray());
            return new ServiceResult<Contact>()
                       {
                           Success = true,
                           Data = contacts
                       };
        }
    }
}
using System.IO;

namespace Contax.Web.UI.Services
{
    public interface IUploadContacts
    {
        ServiceResult Upload(Stream inputStream);
    }
}
using Contax.Web.UI.Domain;

namespace Contax.Web.UI.Services
{
    public interface IContactRepository
    {
        void Save(params Contact[] contacts);
    }
}
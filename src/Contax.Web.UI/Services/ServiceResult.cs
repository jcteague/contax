using System.Collections;
using System.Collections.Generic;

namespace Contax.Web.UI.Services
{
    public class ServiceResult
    {
        public bool Success { get; set; }

        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
using System.Collections;
using System.Collections.Generic;

namespace Contax.Web.UI.Services
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }

        public IEnumerable<string> ErrorMessages { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
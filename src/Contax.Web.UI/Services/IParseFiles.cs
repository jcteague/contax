using System;
using System.Collections.Generic;
using System.IO;

namespace Contax.Web.UI.Services
{
    public interface IParseFiles
    {
        IEnumerable<T> Parse<T>(Stream inputStream,  Action<dynamic> configure = null) where T: class ;
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace Contax.Web.UI.Services
{
    public class CsvParser : IParseFiles
    {
        CsvClassMap[] mappings;
        CsvConfiguration configuration;

        public CsvParser()
        {
            configuration = new CsvConfiguration()
                                {
                                    HasHeaderRecord = true,
                                    IsCaseSensitive = false,
                                };
        }

        public IEnumerable<T> Parse<T>(Stream inputStream, Action<dynamic> configure = null) where T: class
        {
            if (configure != null) configure(configuration);
            
            var cvs_helper = new CsvReader(new StreamReader(inputStream),configuration);
            
            return cvs_helper.GetRecords<T>();
            
        }
    }
}
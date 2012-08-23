using System;
using System.Collections;
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
        IParseFiles _parser;

        public ContactCsvUploader(IParseFiles parser)
        {
            _parser = parser;
        }

        public ServiceResult<Contact> Upload(Stream inputStream)
        {

            var contacts = _parser.Parse<Contact>(inputStream,
                                                  (configuration) =>
                                                  configuration.ClassMapping<Contact, ContactCsvMap>());
            return new ServiceResult<Contact>()
                       {
                           Success = true,
                           Data = contacts
                       };
        }
    }

    public interface IParseFiles
{
    IEnumerable<T> Parse<T>(Stream inputStream,  Action<dynamic> configure = null) where T: class ;
}

    public class Parser : IParseFiles
    {
        CsvClassMap[] mappings;
        CsvConfiguration configuration;

        public Parser()
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
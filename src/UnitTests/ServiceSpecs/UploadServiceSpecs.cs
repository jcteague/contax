using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Contax.Web.UI.Domain;
using Contax.Web.UI.Services;
using FizzWare.NBuilder;
using Machine.Specifications;
using Rhino.Mocks.Constraints;
using developwithpassion.specifications.rhinomocks;
using Rhino.Mocks;
using Machine.Fakes;

namespace UnitTests.ServiceSpecs
{
    public class UploadServiceSpecs : Observes<ContactCsvUploader>
    {
        Establish context = () =>
                                {
                                    contacts = Builder<Contact>.CreateListOfSize(3).Build();
                                    parser = depends.on<IParseFiles>();
                                    inputStream = new MemoryStream();
                                    parser.Stub(x => x.Parse<Contact>(inputStream)).Return(contacts).IgnoreArguments();
                                    contact_repo = depends.on<IContactRepository>();
                                };

        Because of = () =>
                         {
                             result = sut.Upload(inputStream);
                         };


        It should_create_contacts_from_the_input_file = () => result.Data.ShouldEqual(contacts);

        
        It should_validate_the_contacts_uploaded;

        It should_save_the_contacts = () => contact_repo.WasToldTo(x=> x.Save(contacts.ToArray()));
        static Stream input_stream;
        static ServiceResult<Contact> result;
        static List<Contact> data;
        static IParseFiles parser;
        static IList<Contact> contacts;
        static Stream inputStream;
        static IContactRepository contact_repo;
    }
}
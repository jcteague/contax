using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Contax.Web.UI.Domain;
using Contax.Web.UI.Services;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;

namespace UnitTests.ServiceSpecs
{
    public class UploadServiceSpecs : Observes<ContactCsvUploader>
    {
        Establish context = () =>
                                {
                                    var input = String.Join(Environment.NewLine,
                                                            "First Name,Last Name,email,phone,facebook,twitter",
                                                           "John,Teague,jcteague@gmail.com,555-555-5555,jcteague,john_teague");

                                    input_stream = new MemoryStream(Encoding.UTF8.GetBytes(input));
//                                    var writer = new StreamWriter(input_stream);
//                                    writer.WriteLine("First Name,Last Name,email,phone,facebook,twitter");
//                                    writer.WriteLine("John,Teague,jcteague@gmail.com,555-555-5555,jcteague,john_teague");
//                                    writer.Flush();
//                                    input_stream.Position = 0;


                                };

        

        Because of = () =>
                         {
                             data = sut.Upload(input_stream).Data.ToList();
                         };


        It should_create_contacts_from_the_input_file = () => data.Count.ShouldEqual(1);

        It should_set_the_fields_on_the_contact_correctly = () =>
                                                                {
                                                                    data[0].FirstName.ShouldEqual("John");
                                                                    data[0].TwitterUserName.ShouldEqual("john_teague");
                                                                };

        It should_validate_the_contacts_uploaded;

        It should_save_the_contacts;
        static Stream input_stream;
        static ServiceResult<Contact> result;
        static List<Contact> data;
    }
}
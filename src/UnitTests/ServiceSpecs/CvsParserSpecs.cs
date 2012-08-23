using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Contax.Web.UI.Domain;
using Contax.Web.UI.Services;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using System.Linq;

namespace UnitTests.ServiceSpecs
{
    public class CvsParserSpecs : Observes<CsvParser>
    {
        public class CsvParseTestModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
        }
        Establish context = () =>
        {
            var input = String.Join(Environment.NewLine,
                                    "FirstName,LastName,email",
                                   "John,Teague,jcteague@gmail.com",
                                   "Bill,Gates,bill@microsoft.com");

            input_stream = new MemoryStream(Encoding.UTF8.GetBytes(input));
        };

        Because of = () => result = sut.Parse<CsvParseTestModel>(input_stream).ToList();

        It should_parse_the_correct_number_of_records = () => result.Count.ShouldEqual(2);

        It should_populate_the_fields_correctly = () =>
                                                      {
                                                          result[0].FirstName.ShouldEqual("John");
                                                          result[1].Email.ShouldEqual("bill@microsoft.com");

                                                      };
        static MemoryStream input_stream;
        static List<CsvParseTestModel> result;
    }
}
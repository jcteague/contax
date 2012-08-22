using System.Web;
using System.Web.Mvc;
using Contax.Web.UI.Controllers;
using Contax.Web.UI.Domain;
using Contax.Web.UI.Services;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using Rhino.Mocks;

namespace UnitTests.ControllerSpecs
{
    public class ContactUploadSpecs :Observes<ContactController>
    {
        Establish context = () =>
                                {
                                    http_file = fake.an<HttpPostedFileBase>();
                                    contact_file_service = depends.on<IUploadContacts>();
                                    
                                };

        protected static HttpPostedFileBase http_file;
        protected static IUploadContacts contact_file_service;
        protected static ViewResult action_result;
        
        Because of = () => action_result = (ViewResult)sut.Upload(http_file);

        

        

    }
    public class when_the_service_parses_the_upload_file :ContactUploadSpecs
    {
        Establish context = () => contact_file_service
                                      .Stub(x => x.Upload(http_file.InputStream))
                                      .Return(new ServiceResult<Contact> { Success = true });

        It should_return_the_success_view_when_upload_succeeded =
            () => action_result.ViewName.ShouldEqual("UploadCompleted");

    }
    public class when_the_service_does_not_parse_the_upload_file : ContactUploadSpecs {
        Establish context = () => contact_file_service
                                      .Stub(x => x.Upload(http_file.InputStream))
                                      .Return(new ServiceResult<Contact> { Success = false, ErrorMessages = new[] { "Error" } });

        Because of = () => action_result = (ViewResult)sut.Upload(http_file);
        It should_return_empty_view_name = () => action_result.ViewName.ShouldBeEmpty();

        It should_add_the_error_message_to_the_model_state = () => action_result.ViewData.ModelState.IsValid.
                                                                       ShouldBeFalse();

    }
}
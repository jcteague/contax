using System;
using System.Web;
using System.Web.Mvc;
using Contax.Web.UI.Services;

namespace Contax.Web.UI.Controllers
{
    public class ContactController: Controller
    {
        readonly IUploadContacts _uploader;

        public ContactController(IUploadContacts uploader)
        {
            _uploader = uploader;
        }

        public ActionResult Upload(HttpPostedFileBase file)
        {
            var upload_result = _uploader.Upload(file.InputStream);
            if(upload_result.Success)
                return View("UploadCompleted");
            else
            {
                foreach (var message in upload_result.ErrorMessages)
                {
                    ModelState.AddModelError("Upload Error",message);
                    
                }
                return View();
            }
        }
    }
}
using System.IO;
using System.Web.Mvc;

namespace BayiPuan.MvcWebUi.GenericVM
{
    public class UploadModelBinder : ByteArrayModelBinder
    {
        public override object BindModel(ControllerContext cc, ModelBindingContext bc)
        {
            var file = cc.HttpContext.Request.Files[bc.ModelName];
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var ext = Path.GetExtension(file.FileName);
                    cc.Controller.ViewData["GenericBindingMessage"] = new GenericBindingMessage
                    {
                        PropertyName = bc.ModelName,
                        Extension = ext,
                        ContentType = file.ContentType
                    };

                    var fileBytes = new byte[file.ContentLength];
                    file.InputStream.Read(fileBytes, 0, fileBytes.Length);
                    return fileBytes;
                }
                return null;
            }
            return base.BindModel(cc, bc);
        }
    }

    public class GenericBindingMessage
    {
        public string PropertyName;
        public string ContentType;
        public string Extension;
    }
}
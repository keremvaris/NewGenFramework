using System.Linq;
using System.Web.Mvc;

namespace BayiPuan.MvcWebUi.GenericVM
{
    public class CustomModelBinder : DefaultModelBinder
    {
        protected override void OnModelUpdated(ControllerContext cc, ModelBindingContext bc)
        {
            base.OnModelUpdated(cc, bc);
            // File Upload varsa ozel alanlari doldur
            object v;
            if(cc.Controller.ViewData.TryGetValue("GenericBindingMessage", out v))
            {
                var gbm = (GenericBindingMessage)v;
                var meta = CrudExtensions.Get(bc.ModelType);
                var ec = gbm.PropertyName + "Ext";
                var prop = meta.Columns.FirstOrDefault(c => c.Property.Name == ec).Property;
                if(prop!=null)
                    prop.SetValue(bc.Model, gbm.Extension);
            }
        }
    }
}
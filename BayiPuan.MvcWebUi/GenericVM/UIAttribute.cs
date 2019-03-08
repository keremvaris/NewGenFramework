using System;

namespace BayiPuan.MvcWebUi.GenericVM
{
    public class UIAttribute : Attribute
    {
        public string CascadeParentColumnName { get; set; }
    public bool IsMaster { get; set; }

      public UIAttribute(string cascadeParentColumnName)
        {
            CascadeParentColumnName = cascadeParentColumnName;
        }
    }
}
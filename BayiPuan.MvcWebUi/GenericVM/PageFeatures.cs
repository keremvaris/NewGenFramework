using System;

namespace BayiPuan.MvcWebUi.GenericVM
{
    public class PageFeatures:Attribute
    {
        public PageFeatures()
        {
            Caption = "";
            Visible = false;
        }
        public string Caption { get; set; }
        public bool Visible { get; set; }

    }
}
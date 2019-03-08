using System.Web;

namespace BayiPuan.MvcWebUi.Localize
{
    public  class SetUserLocale
    {
        /// <summary>
        /// Sets a user's Locale based on the browser's Locale setting. If no setting
        /// is provided the default Locale is used.
        /// </summary>
        public static void SetUserLocales(bool SetUiCulture)
        {
            HttpRequest Request = HttpContext.Current.Request;
            if (Request.UserLanguages == null)
                return;

            string Lang = Request.UserLanguages[0];
            if (Lang != null)
            {
                // *** Problems with Turkish Locale and upper/lower case
                // *** DataRow/DataTable indexes
                if (Lang.StartsWith("tr"))
                    return;

                if (Lang.Length < 3)
                    Lang = Lang + "-" + Lang.ToUpper();
                try
                {
                    System.Globalization.CultureInfo Culture = new System.Globalization.CultureInfo(Lang);
                    System.Threading.Thread.CurrentThread.CurrentCulture = Culture;
                    

                    if (SetUiCulture)
                        System.Threading.Thread.CurrentThread.CurrentUICulture = Culture;
                }
                catch
                {; }
            }
        }
    }
}
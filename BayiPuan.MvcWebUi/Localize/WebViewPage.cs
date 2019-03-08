using System.Web.Mvc;


namespace BayiPuan.MvcWebUi.Localize
{
    public delegate LocalizedString Localizer(string localized, params object[] args);

    public abstract class WebViewBase : WebViewPage<dynamic>
    {
    }
    public abstract class WebViewBase<T> : WebViewPage<T>
    {
        private Localizer _localizer;

        public Localizer Word
        {
            get
            {
                return _localizer ?? (_localizer = (word, parameters) =>
                {
                    if (parameters == null || parameters.Length == 0)
                    {
                        return new LocalizedString(word);
                    }
                    return new LocalizedString(string.Format(word, parameters));
                });
            }
        }
    }
}

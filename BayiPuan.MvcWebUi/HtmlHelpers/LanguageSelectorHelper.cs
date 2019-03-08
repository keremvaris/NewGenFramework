using System.Text;
using System.Web;
using System.Web.Mvc;
using BayiPuan.Business.Abstract;
using BayiPuan.Business.DependencyResolvers.Ninject;


namespace BayiPuan.MvcWebUi.HtmlHelpers
{
    public static class LanguageSelectorHelper
    {
        public static MvcHtmlString LangSelector(this HtmlHelper helper)
        {
            var languageService = DependencyResolver<ILanguageService>.Resolve();
            var langCollection = languageService.GetAll();
            
            var sb=new StringBuilder();
            sb.Append(@"<div class='w3-dropdown-hover'>");
            sb.Append(@"<button class='w3-button'><i class='fa fa-language fa-fw'></i></button>");
            sb.Append("<div class='w3-dropdown-content w3-bar-block w3-border'>");
            foreach (var item in langCollection)
            {
                
                sb.Append("<a href ='../Home/ChangeCulture?dilId=" + item.LanguageId + "&lang=" + item.Code +
                          "&returnUrl="+ HttpContext.Current.Request.RawUrl + "'>" + item.Name + "</a>");
                sb.Append("<br/>");
            }
            sb.Append("</div>");
            sb.Append("</div>");
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}
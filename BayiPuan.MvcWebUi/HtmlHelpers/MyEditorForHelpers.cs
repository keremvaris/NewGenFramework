using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace BayiPuan.MvcWebUi.HtmlHelpers
{
    public static class MyEditorForHelpers
    {
        public static IHtmlString MyEditorFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object ViewData, bool disabled = false, bool visible = true)
        {
            var member = expression.Body as MemberExpression;
            var stringLength = member.Member.GetCustomAttributes(typeof(StringLengthAttribute), false).FirstOrDefault() as StringLengthAttribute;

            RouteValueDictionary viewData = HtmlHelper.AnonymousObjectToHtmlAttributes(ViewData);
            RouteValueDictionary htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(viewData["htmlAttributes"]);

            if (stringLength != null)
            {
                htmlAttributes.Add("maxlength", stringLength.MaximumLength);
            }

            return htmlHelper.TextBoxFor(expression, htmlAttributes); // use custom HTML attributes here
        }
    }
}
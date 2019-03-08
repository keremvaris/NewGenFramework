using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace BayiPuan.MvcWebUi.HtmlHelpers
{
  public static class MyCheckBoxForHelpers
  {
    public static MvcHtmlString MyCheckBoxFor<TModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, bool>> expression)
    {

      TagBuilder innerContainer = new TagBuilder("div");
      innerContainer.AddCssClass("col-sm-5");
      innerContainer.InnerHtml = helper.CheckBoxFor(expression, new { @class = "form-control" }).ToString();

      StringBuilder html = new StringBuilder();
      html.Append(helper.LabelFor(expression, new { @class = "col-sm-3 control-label" }));
      html.Append(innerContainer.ToString());

      TagBuilder outerContainer = new TagBuilder("div");
      outerContainer.AddCssClass("form-group");
      outerContainer.InnerHtml = html.ToString();

      return MvcHtmlString.Create(outerContainer.ToString());

    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ISCJ.TagHelpers
{
  public static class HtmlHelpers
  {
        public static string IsPageSelected(this IHtmlHelper html, string pageUrl, string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string url = (string)html.ViewContext.RouteData.Values["page"];

           

            if (url.Contains(pageUrl))
            {
                return cssClass;
            }
            else
            {
                return string.Empty;
            }
        }

        public static bool IsPageActive(string requestUrl, string pageUrl, string cssClass = null)
        {
            if (requestUrl.ToLower().Contains(pageUrl.ToLower()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static string IsSelected(this IHtmlHelper html, string controller = null, string action = null, string cssClass = null)
    {
      if (String.IsNullOrEmpty(cssClass))
        cssClass = "active";

      string currentAction = (string)html.ViewContext.RouteData.Values["action"];
      string currentController = (string)html.ViewContext.RouteData.Values["controller"];

      if (String.IsNullOrEmpty(controller))
        controller = currentController;

      if (String.IsNullOrEmpty(action))
        action = currentAction;

      return controller == currentController && action == currentAction ?
          cssClass : String.Empty;
    }

    public static string PageClass(this IHtmlHelper htmlHelper)
    {
      string currentAction = (string)htmlHelper.ViewContext.RouteData.Values["action"];
      return currentAction;
    }

  }
}

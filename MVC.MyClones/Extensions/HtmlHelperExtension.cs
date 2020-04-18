using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace MVC.MyClones.Extension
{
    public static class HtmlHelperExtension
    {
        private const string _partialViewScriptItemPrefix = "scripts_";
        private const string _partialViewStylesItemPrefix = "styles_";

        /// <summary>
        /// Рендер скриптовых блоков, вызывается во вьюхах
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="template">Блок скрипта</param>
        /// <returns></returns>
        public static IHtmlContent PartialSectionScripts(this IHtmlHelper htmlHelper, Func<object, HelperResult> template)
        {
            htmlHelper.ViewContext.HttpContext.Items[_partialViewScriptItemPrefix + Guid.NewGuid()] = template;
            return new HtmlContentBuilder();
        }

        /// <summary>
        /// Рендер скриптовых блоков, вызывается в лейаутах
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static IHtmlContent RenderPartialSectionScripts(this IHtmlHelper htmlHelper)
        {
            var partialSectionScripts = htmlHelper.ViewContext.HttpContext.Items.Keys
                .Where(k => Regex.IsMatch(
                    k.ToString(),
                    "^" + _partialViewScriptItemPrefix + "([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})$"));
            var contentBuilder = new HtmlContentBuilder();
            foreach (var key in partialSectionScripts)
            {
                var template = htmlHelper.ViewContext.HttpContext.Items[key] as Func<object, HelperResult>;
                if (template != null)
                {
                    var writer = new System.IO.StringWriter();
                    template(null).WriteTo(writer, HtmlEncoder.Default);
                    contentBuilder.AppendHtml(writer.ToString());
                }
            }
            return contentBuilder;
        }

        public static IHtmlContent PartialSectionStyles(this IHtmlHelper htmlHelper, Func<object, HelperResult> template)
        {
            htmlHelper.ViewContext.HttpContext.Items[_partialViewStylesItemPrefix + Guid.NewGuid()] = template;
            return new HtmlContentBuilder();
        }

        public static IHtmlContent RenderPartialSectionStyles(this IHtmlHelper htmlHelper)
        {
            var partialSectionStyles = htmlHelper.ViewContext.HttpContext.Items.Keys
                .Where(k => Regex.IsMatch(
                    k.ToString(),
                    "^" + _partialViewStylesItemPrefix + "([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})$"));
            var contentBuilder = new HtmlContentBuilder();
            foreach (var key in partialSectionStyles)
            {
                var template = htmlHelper.ViewContext.HttpContext.Items[key] as Func<object, HelperResult>;
                if (template != null)
                {
                    var writer = new System.IO.StringWriter();
                    template(null).WriteTo(writer, HtmlEncoder.Default);
                    contentBuilder.AppendHtml(writer.ToString());
                }
            }
            return contentBuilder;
        }
    }
}

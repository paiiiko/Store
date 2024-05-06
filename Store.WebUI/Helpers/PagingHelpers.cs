using Store.WebUI.ViewModels;
using System.Text;
using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web.WebPages;

namespace Store.WebUI.Helpers
{
    public static class PagingHelpers
    {
        public static HtmlString PageLinks(this IHtmlHelper html,
                                                PageInfo pageInfo,
                                                Func<int, string> pageUrl)
        {
            string sortOrder = html.ViewContext.HttpContext.Request.QueryString.Value.ToString();
            List<TagBuilder> tags = new List<TagBuilder>();
            StringBuilder result = new StringBuilder();
            TagBuilder arrow = new TagBuilder("a");
            TagBuilder arrow2 = new TagBuilder("a");
            TagBuilder svg = new TagBuilder("svg");
            TagBuilder path = new TagBuilder("path");

            path.Attributes.Add("d", "m121.3,34.6c-1.6-1.6-4.2-1.6-5.8," +
                                     "0l-51,51.1-51.1-51.1c-1.6-1.6-4.2-1.6-5.8," +
                                     "0-1.6,1.6-1.6,4.2 0,5.8l53.9,53.9c0.8,0.8 1.8,1.2 2.9,1.2 1," +
                                     "0 2.1-0.4 2.9-1.2l53.9-53.9c1.7-1.6 1.7-4.2 0.1-5.8z");
            svg.Attributes.Add("version", "1.1");
            svg.Attributes.Add("xmlns", "http://www.w3.org/2000/svg");
            svg.Attributes.Add("viewBox", "0 0 129 129");
            svg.Attributes.Add("xmlns:xlink", "http://www.w3.org/1999/xlink");
            svg.Attributes.Add("enable-background", "new 0 0 129 129");
            svg.InnerHtml.AppendHtml(path);
            arrow.Attributes.Add("href", pageUrl(pageInfo.PageNumber - 1) + sortOrder);
            arrow2.Attributes.Add("href", pageUrl(pageInfo.PageNumber + 1) + sortOrder);
            arrow.InnerHtml.AppendHtml(svg);
            arrow2.InnerHtml.AppendHtml(svg);

            for (int i = 0; i <= pageInfo.TotalPages + 1; i++)
            {
                if (i == 0)
                {
                    tags.Add(arrow);
                    continue;
                }
                if (i == pageInfo.TotalPages + 1)
                {
                    tags.Add(arrow2);
                    continue;
                }
                TagBuilder tag = new TagBuilder("a");
                tag.InnerHtml.SetContent(i.ToString());
                tag.MergeAttribute("href", pageUrl(i) + sortOrder);
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("active");
                }
                if (pageInfo.PageNumber == 1)
                {
                    tags[0].AddCssClass("disabled");
                }
                if (pageInfo.PageNumber == pageInfo.TotalPages)
                {
                    arrow2.AddCssClass("disabled");
                }
                tags.Add(tag);
            }

            foreach (var tag in tags)
            {
                using (var writer = new StringWriter())
                {

                    tag.WriteTo(writer, HtmlEncoder.Default);
                    result.Append(writer.ToString() + "\n");
                }
            }
            return new HtmlString(result.ToString());
        }
    }
}
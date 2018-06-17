using StudentTestingSystem.Services.Extensions;
using System;
using System.Text;
using System.Web.Mvc;

namespace StudentTestingSystem.Helpers
{
    public static class PaginationHelper
    {
        public static MvcHtmlString PageLinks<T>(this HtmlHelper html, PageInfo<T> pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            if (pageInfo.TotalPages > 1)
            {
                TagBuilder ulTag = new TagBuilder("ul");
                ulTag.AddCssClass("pagination justify-content-center");

                TagBuilder liTagPrevious = new TagBuilder("li");
                TagBuilder aTagPrevious = new TagBuilder("a");
                liTagPrevious.AddCssClass("page-item");
                if (pageInfo.PageNumber == 1)
                {
                    liTagPrevious.AddCssClass("disabled");
                    aTagPrevious.MergeAttribute("href", "#");
                    aTagPrevious.MergeAttribute("tabindex", "-1");
                }
                else
                {
                    aTagPrevious.MergeAttribute("href", pageUrl(pageInfo.PageNumber - 1));
                }
                aTagPrevious.AddCssClass("page-link");
                aTagPrevious.InnerHtml = "Previous";
                liTagPrevious.InnerHtml += aTagPrevious;
                ulTag.InnerHtml += liTagPrevious;

                for (int i = 1; i <= pageInfo.TotalPages; i++)
                {
                    TagBuilder liTag = new TagBuilder("li");
                    liTag.AddCssClass("page-item");

                    TagBuilder aTag = new TagBuilder("a");
                    aTag.MergeAttribute("href", pageUrl(i));
                    aTag.AddCssClass("page-link");
                    aTag.InnerHtml = i.ToString();

                    if (i == pageInfo.PageNumber)
                    {
                        liTag.AddCssClass("active");
                        TagBuilder spanTag = new TagBuilder("span");
                        spanTag.AddCssClass("sr-only");
                        spanTag.InnerHtml = "(current)";
                        aTag.InnerHtml += spanTag;
                    }

                    liTag.InnerHtml += aTag;
                    ulTag.InnerHtml += liTag;
                }

                TagBuilder liTagNext = new TagBuilder("li");
                TagBuilder aTagNext = new TagBuilder("a");
                liTagNext.AddCssClass("page-item");
                if (pageInfo.PageNumber == pageInfo.TotalPages)
                {
                    liTagNext.AddCssClass("disabled");
                    aTagNext.MergeAttribute("href", "#");
                    aTagNext.MergeAttribute("tabindex", "-1");
                }
                else
                {
                    aTagNext.MergeAttribute("href", pageUrl(pageInfo.PageNumber + 1));
                }
                aTagNext.AddCssClass("page-link");
                aTagNext.InnerHtml = "Next";

                liTagNext.InnerHtml += aTagNext;
                ulTag.InnerHtml += liTagNext;
                result.Append(ulTag);
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}
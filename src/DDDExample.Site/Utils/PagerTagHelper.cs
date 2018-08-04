using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DDDExample.Site.Utils
{
    [HtmlTargetElement("pager")]
    public class PagerTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PagerTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public dynamic PagedResult { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var listBuilder = new StringBuilder();

            var totalPages = (int)Math.Ceiling((double)PagedResult.TotalNumberOfItems / PagedResult.Paging.Top);

            var pagingIndexes = GetPagingIndexes(
                PagedResult.Paging.Skip / PagedResult.Paging.Top,
                totalPages);

            if (totalPages > 1)
            {
                for (var i = 0; i < pagingIndexes.Length; i++)
                {
                    if (i > 0 && pagingIndexes[i - 1] != pagingIndexes[i] - 1)
                    {
                        listBuilder.AppendLine("<li class=\"page-item disabled\"><a href=\"#\" class=\"page-link\">&hellip;</a></li>");
                    }

                    if (PagedResult.Paging.Skip / PagedResult.Paging.Top == pagingIndexes[i])
                    {
                        listBuilder.AppendLine("<li class=\"page-item active\"><a href=\"#\" class=\"page-link\">" + (pagingIndexes[i] + 1) + "</a></li>");
                    }
                    else
                    {
                        var url = _httpContextAccessor.HttpContext.Request.QueryString.Value;
                        string skip = (pagingIndexes[i] * PagedResult.Paging.Top).ToString();
                        url = url.SetParameters(KeyValuePair.Create("skip", skip));

                        listBuilder.AppendLine("<li class=\"page-item\"><a href=\"" + url + "\" class=\"page-link\">" + (pagingIndexes[i] + 1) + "</a></li>");
                    }
                }
            }

            output.TagName = "ul";
            output.Attributes.Add("class", "pagination");
            output.Content.SetHtmlContent(listBuilder.ToString());

            output.PreElement.SetHtmlContent("<nav>");
            output.PostElement.SetHtmlContent("</nav>");
        }

        private static int[] GetPagingIndexes(int currentIndex, int totalPages)
        {
            var result = new HashSet<int>();

            for (var i = 0; i < 2; i++)
            {
                if (i > totalPages) continue;
                result.Add(i);
            }

            var current = currentIndex - 2;

            while (current <= currentIndex + 2)
            {
                if (current > 0 && current < totalPages) result.Add(current);
                current++;
            }

            for (var i = totalPages - 2; i < totalPages; i++) result.Add(i);

            return result.ToArray();
        }
    }
}
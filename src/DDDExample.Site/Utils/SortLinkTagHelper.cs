using System.Collections.Generic;
using DDDExample.SharedKernel.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DDDExample.Site.Utils
{
    [HtmlTargetElement("a")]
    public class SortLinkTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SortLinkTagHelper(IHttpContextAccessor httpContextAccessor) =>
            _httpContextAccessor = httpContextAccessor;

        public dynamic Paging { get; set; }

        public string PropertyName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var url = _httpContextAccessor.HttpContext.Request.QueryString.Value;

            var extendedUrl = url.SetParameters(
                KeyValuePair.Create("sortColumn", PropertyName),
                KeyValuePair.Create("sortDirection", GetSortDirection().ToString()),
                KeyValuePair.Create("skip", "0"));

            output.Attributes.Add("href", extendedUrl);
        }

        private SortDirection GetSortDirection()
        {
            var sortDirection = SortDirection.Ascending;

            if (Paging != null
                && PropertyName.Equals(Paging.SortColumn)
                && Paging?.SortDirection == SortDirection.Ascending)
            {
                sortDirection = SortDirection.Descending;
            }

            return sortDirection;
        }
    }
}
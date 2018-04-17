using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doppler.Mobile.Core.Models.Dto
{
    public class PageDto<T>
    {
        [JsonProperty(PropertyName = "items", Required = Required.DisallowNull)]
        public IList<T> Items { get; set; }

        [JsonProperty(PropertyName = "pageSize", Required = Required.DisallowNull)]
        public int PageSize { get; set; }

        [JsonProperty(PropertyName = "itemsCount", Required = Required.DisallowNull)]
        public int ItemsCount { get; set; }

        [JsonProperty(PropertyName = "currentPage", Required = Required.DisallowNull)]
        public int CurrentPage { get; set; }

        [JsonProperty(PropertyName = "pagesCount", Required = Required.DisallowNull)]
        public int PagesCount { get; set; }
    }
}

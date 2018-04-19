using System.Collections.Generic;

namespace Doppler.Mobile.Core.Models
{
    public class Page<T>
    {
        public IList<T> Items { get; set; }

        public int PageSize { get; set; }

        public int ItemsCount { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }
    }
}

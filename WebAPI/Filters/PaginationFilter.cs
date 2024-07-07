using System.ComponentModel;

namespace WebAPI.Filters
{
    public class PaginationFilter
    {
        private const int maxPageSize = 10;

        /// <summary>
        /// Indicates which page is to be displayed (default: 1).
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Indicates how many elements should be displayed on the page (default: 10).
        /// </summary>
        public int PageSize { get; set; }

        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = maxPageSize;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > maxPageSize ? maxPageSize : pageSize;
        }
    }
}

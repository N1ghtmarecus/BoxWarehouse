using System.ComponentModel;
using WebAPI.Helpers;

namespace WebAPI.Filters
{
    public class SortingFilter
    {
        /// <summary>
        /// Sets the field to sort by (default: CutterID).
        /// </summary>
        public string? SortField { get; set; }

        /// <summary>
        /// A value indicating whether the sorting is in ascending or descending order (default: true).
        /// </summary>
        public bool Ascending { get; set; } = true;

        public SortingFilter()
        {
            SortField = "CutterID";
        }

        public SortingFilter(string? sortField, bool ascending)
        {
            var sortFields = SortingHelper.GetSortField();

            sortField = sortField?.ToLower();

            if (sortFields.Select(x => x.Key).Contains(sortField?.ToLower()))
                sortField = sortFields.Where(x => x.Key == sortField).Select(x => x.Value).SingleOrDefault();
            else
                sortField = "CutterID";

            SortField = sortField;
            Ascending = ascending;
        }
    }
}
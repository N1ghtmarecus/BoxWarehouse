﻿using WebAPI.Helpers;

namespace WebAPI.Filters
{
    public class SortingFilter
    {
        public string? SortField { get; set; }
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
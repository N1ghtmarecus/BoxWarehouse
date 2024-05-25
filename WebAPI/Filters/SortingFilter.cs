namespace WebAPI.Filters
{
    public class SortingFilter
    {
        public string? SortField { get; set; }
        public bool Ascending { get; set; } = true;

        public SortingFilter()
        {
            SortField = "id";
        }
    }
}
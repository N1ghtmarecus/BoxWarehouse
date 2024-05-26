namespace WebAPI.Helpers
{
    public class SortingHelper
    {
        public static KeyValuePair<string, string>[] GetSortField()
        {
            return new[] { SortFields.CutterID, SortFields.CreationDate };
        }
    }

    public class SortFields
    {
        public static KeyValuePair<string, string> CutterID { get; } = new("cutterID", "CutterID");
        public static KeyValuePair<string, string> CreationDate { get; } = new("creationdate", "Created");
    }
}

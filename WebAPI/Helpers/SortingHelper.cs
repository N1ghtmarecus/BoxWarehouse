namespace WebAPI.Helpers
{
    public class SortingHelper
    {
        public static KeyValuePair<string, string>[] GetSortField()
        {
            return [SortFields.CutterID, SortFields.Length, SortFields.Width, SortFields.Height, SortFields.CreationDate];
        }
    }

    public class SortFields
    {
        public static KeyValuePair<string, string> CutterID { get; } = new("cutterID", "CutterID");
        public static KeyValuePair<string, string> Length { get; } = new("length", "Length");
        public static KeyValuePair<string, string> Width { get; } = new("width", "Width");
        public static KeyValuePair<string, string> Height { get; } = new("height", "Height");
        public static KeyValuePair<string, string> CreationDate { get; } = new("creationdate", "Created");
    }
}

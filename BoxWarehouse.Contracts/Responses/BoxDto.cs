namespace BoxWarehouse.Contracts.Responses
{
    public class BoxDto
    {
        public int CutterID { get; set; }
        public int Fefco { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public DateTime CreationDate { get; set; }
    }
}

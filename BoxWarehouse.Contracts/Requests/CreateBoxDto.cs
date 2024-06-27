namespace BoxWarehouse.Contracts.Requests
{
    public class CreateBoxDto
    {
        public int CutterID { get; set; }
        public int Fefco { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}

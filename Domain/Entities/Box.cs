using Domain.Common;

namespace Domain.Entities
{
    public class Box : AuditableEntity
    {
        public int CutterID { get; set; }
        public int Fefco { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Box() { }

        public Box(int cutterID, int fefco, int length, int width, int height)
        {
            CutterID = cutterID;
            Fefco = fefco;
            Length = length;
            Width = width;
            Height = height;
        }
    }
}

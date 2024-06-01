using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Boxes")]
    public class Box : AuditableEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(3)]
        public int CutterID { get; set; }

        [Required]
        [MaxLength(3)]
        public int Fefco { get; set; }

        [Required]
        [MaxLength(4)]
        public int Length { get; set; }

        [Required]
        [MaxLength(4)]
        public int Width { get; set; }

        [Required]
        [MaxLength(4)]
        public int Height { get; set; }

        [Required]
        [MaxLength(450)]
        public string? UserId { get; set; }

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

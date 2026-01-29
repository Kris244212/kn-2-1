using System;
using System.ComponentModel.DataAnnotations;

namespace lab22.Models
{
    public class ShapeRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ShapeType { get; set; } // "Rectangle" або "Square"

        [Required]
        public int Dimension1 { get; set; } // Width для Rectangle, Size для Square

        public int? Dimension2 { get; set; } // Height для Rectangle (null для Square)

        public int CalculatedArea { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return ShapeType switch
            {
                "Rectangle" => $"ID: {Id} | Rectangle: Width={Dimension1}, Height={Dimension2} | Area={CalculatedArea}",
                "Square" => $"ID: {Id} | Square: Size={Dimension1} | Area={CalculatedArea}",
                _ => $"ID: {Id} | {ShapeType} | Area={CalculatedArea}"
            };
        }
    }
}

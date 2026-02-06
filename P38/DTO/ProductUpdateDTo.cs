using System.ComponentModel.DataAnnotations;

namespace P38.DTO
{
    public class ProductUpdateDTO
    {
        [Required, MinLength(3)]
        public string? Brand { get; set; }
        [Required, MinLength(3)]
        public string? Model { get; set; }
        public string? Description { get; set; }
        public string? FK_Salesman { get; set; }

        [Range(0, 999999999)]
        public decimal? Price { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        public string? currency { get; set; }

        public CharacteristicsDTO? Characteristics { get; set; }

        
    }
}

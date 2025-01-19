using System.ComponentModel.DataAnnotations;

namespace CarStore.Models.Dto
{
    public class CarCreateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Model { get; set; }

        [Required]
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }
    }
}

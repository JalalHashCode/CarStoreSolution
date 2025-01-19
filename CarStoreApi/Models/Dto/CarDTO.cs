﻿using System.ComponentModel.DataAnnotations;

namespace CarStoreApi.Models.Dto
{
    public class CarDTO
    {
        [Required]
        public int Id { get; set; }
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

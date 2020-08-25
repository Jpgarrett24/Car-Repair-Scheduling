using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarRepairScheduling.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [Range(0, 2000000000)]
        public int Mileage { get; set; }

        [Required]
        [Range(1800, 3000)]
        public int Year { get; set; }

        public int UserId { get; set; }
        public User Owner { get; set; }

        public List<Service> Services { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
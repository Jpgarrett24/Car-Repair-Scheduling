using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarRepairScheduling.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Make is required")]
        [Display(Name = "Make: ")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model is required")]
        [Display(Name = "Model: ")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Milage is required")]
        [Display(Name = "Milage: ")]
        [Range(0, 2000000000, ErrorMessage = "Must be a positive number")]
        public int Mileage { get; set; }


        [Required(ErrorMessage = "Year is required")]
        [Display(Name = "Year: ")]
        [Range(1901, 3000)]
        public int Year { get; set; }

        public int UserId { get; set; }
        public User Owner { get; set; }

        public List<Service> Services { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
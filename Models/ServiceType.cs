using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarRepairScheduling.Models
{
    public class ServiceType
    {
        [Key]
        public int ServiceTypeId { get; set; }

        [Required(ErrorMessage = "Service name is required")]
        [Display(Name = "Service Name: ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Service Price is required")]
        [Display(Name = "Service Price: ")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Service duration is required")]
        [Display(Name = "Service duration: ")]
        [DataType(DataType.Duration)]
        public TimeSpan Duration { get; set; }

        public Boolean Active { get; set; }
        public List<Service> Services { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
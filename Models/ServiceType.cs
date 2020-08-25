using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarRepairScheduling.Models
{
    public class ServiceType
    {
        [Key]
        public int ServiceTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        [DataType(DataType.Duration)]
        public TimeSpan Duration { get; set; }

        public Boolean Active { get; set; }
        public List<Service> Services { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
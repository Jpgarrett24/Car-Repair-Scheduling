using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarRepairScheduling.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        [DataType(DataType.Duration)]
        public TimeSpan Duration { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Start { get; set; }

        public int CarId { get; set; }
        public Car ServicedCar { get; set; }
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
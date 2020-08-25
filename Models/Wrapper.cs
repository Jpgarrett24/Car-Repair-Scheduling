using System.Collections.Generic;

namespace CarRepairScheduling.Models
{
    public class Wrapper
    {
        public LoginUser LoginUser { get; set; }
        public List<User> AllUsers { get; set; }
        public User User { get; set; }
        public Car Car { get; set; }
        public List<Car> AllCars { get; set; }
        public ServiceType ServiceType { get; set; }
        public List<ServiceType> AllServiceTypes { get; set; }
        public Service Service { get; set; }
        public List<Service> AllServices { get; set; }
    }
}
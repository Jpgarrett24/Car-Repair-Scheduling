using System.Collections.Generic;

namespace CarRepairScheduling.Models
{
    public class Wrapper
    {
        public LoginUser LoginUser { get; set; }
        public List<User> AllUsers { get; set; }
        public User User { get; set; }
    }
}
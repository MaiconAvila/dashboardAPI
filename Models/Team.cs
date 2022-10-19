using System.Collections.Generic;

namespace DashboardAPI.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LicensePlate { get; set; }
        public List<Order> Order { get; set; }
    }
}

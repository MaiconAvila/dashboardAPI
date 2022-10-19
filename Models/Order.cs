using System;
using System.Collections.Generic;

namespace DashboardAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime? CreateAt { get; set; } = DateTime.Now;
        public DateTime DeliveryDate { get; set; }
        public string Address { get; set; }
        public List<Product> Product { get; set; }
        public int IdTeam { get; set; }
        public string NameTeam { get; set; }
    }
}

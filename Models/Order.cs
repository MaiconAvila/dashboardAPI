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
        public List<ProductOrder> Product { get; set; }
        public int IdTeam { get; set; }
        public string NameTeam { get; set; }
    }

    public class ProductOrder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}

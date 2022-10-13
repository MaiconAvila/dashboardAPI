using System;
namespace DashboardAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Address { get; set; }
        public int IdProduct { get; set; }
    }
}

using System;
using System.Collections.Generic;
namespace DashboardAPI.Models
{
    public class Demand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Order> Order { get; set; }
        public List<Team> Team { get; set; }
    }
}

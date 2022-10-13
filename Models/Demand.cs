using System;
using System.Collections.Generic;

namespace DashboardAPI.Models
{
    public class Demand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdOrder { get; set; }
        public int IdTeam { get; set; }
    }
}

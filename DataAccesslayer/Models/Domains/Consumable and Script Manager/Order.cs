using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Domains.Consumable_and_Script_Manager
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int ManagerID { get; set; }
        public double TotalCost { get; set; }
        public string Status { get; set; }
    }
}

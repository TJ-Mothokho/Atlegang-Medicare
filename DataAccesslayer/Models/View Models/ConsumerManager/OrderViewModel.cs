using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.View_Models.ConsumerManager
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public int ManagerID { get; set; }
        public DateTime OrderDate { get; set; }

        public int OrderDetailsID { get; set; }
        public int ConsumableID { get; set; }
        public string ConsumableName { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
        public int OrderID_fk { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }

        public IEnumerable<OrderDetail> orderDetails { get; set; }
    }
}

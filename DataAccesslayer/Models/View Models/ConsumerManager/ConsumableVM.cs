using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.View_Models.ConsumerManager
{
    public class ConsumableVM
    {
        public int ConsumableID { get; set; }
        public string ConsumableName { get; set; }
        public double Cost { get; set; }
        public int QuantityOnHand { get; set; }
        public int OnHandThreshold { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public int WardThreshold { get; set; }
    }
}

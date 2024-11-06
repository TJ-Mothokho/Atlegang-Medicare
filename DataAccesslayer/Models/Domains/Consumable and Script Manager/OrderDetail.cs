using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Domains.Consumable_and_Script_Manager
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        [Required]
        public int ConsumableID { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }
        public int  OrderID { get; set; }
        public string Message { get; set; }
    }
}

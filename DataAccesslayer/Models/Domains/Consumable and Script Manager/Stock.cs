using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAccesslayer.Models.Domains.Consumable_and_Script_Manager
{
    public class Stock
    {
        [Key]
        public int WardStockID { get; set; }

        [Required]
        [DisplayName("Ward")]
        public int WardID { get; set; }

        [Required]
        [DisplayName("Consumable")]
        public int ConsumableID { get; set; }

        [Required]
        [DisplayName("Quantity")]
        public int Quantity { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace DataAccesslayer.Models.Domains.Consumable_and_Script_Manager
{
    public class Consumable
    {
        [Key]
        [Required]
        [Display(Name = "Consumable")]
        public int ConsumableID { get; set; }

        [Required]
        [Display(Name = "Consumable Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int QuantityOnHand { get; set; }

        [Required]
        [Display(Name = "Cost")]
        public float Cost { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }

        [Required]
        [Display(Name = "Stock Threshold")]
        public int OnHandThreshold { get; set; }

        [Required]
        [Display(Name = "Threshold in Ward")]
        public int WardThreshold { get; set; }
        public string Status { get; set; }
    }
}

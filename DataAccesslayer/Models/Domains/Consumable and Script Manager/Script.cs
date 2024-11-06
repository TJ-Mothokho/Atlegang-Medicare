

using System.ComponentModel.DataAnnotations;

namespace DataAccesslayer.Models.Domains.Consumable_and_Script_Manager
{
    public class Script
    {
        [Key]
        public int ScriptID { get; set; }


        [Display(Name = "Date Added")]
        public DateTime ScriptDate { get; set; }

        [Display(Name = "Doctor")]
        public int DoctorID { get; set; }

        [Display(Name = "Approved By")]
        public int ManagerID { get; set; }

        [Display(Name = "File Number")]
        public int FileID { get; set; }
        public string Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Domains.Consumable_and_Script_Manager
{
    public class ScriptDetail
    {
        public int ScriptDetailID { get; set; }

        [Display(Name = "Medication")]
        public int MedicationID { get; set; }
        public string Dosage { get; set; }
        public int ScriptID { get; set; }
    }
}

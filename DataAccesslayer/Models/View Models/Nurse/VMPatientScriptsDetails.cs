using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.View_Models.Nurse
{
    public class VMPatientScriptsDetails
    {
        public int ScriptDetailID { get; set; }

        public int MedicationID { get; set; }
        public string Name { get; set; }
        public int Schedule { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime ScriptDate { get; set; }

        public string Dosage { get; set; }
        public int ScriptID { get; set; }

        public string DoctorsName { get; set; }

        public int FileID {  get; set; }    
        public string PatientsName { get; set; }

       
    }
}

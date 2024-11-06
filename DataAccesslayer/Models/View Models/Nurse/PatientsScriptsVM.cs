using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.View_Models.Nurse
{
    public class PatientsScriptsVM
    {
        public int ScriptID { get; set; }


       
        public DateTime ScriptDate { get; set; }

        public int DoctorID { get; set; }
         public string DoctorsName { get; set; }

        public int ManagerID { get; set; }

        public int FileID { get; set; }
        public string PatientsName { get; set; }

        public string Status { get; set; }

        public DateTime MostRecentScriptDate {  get; set; }
        public int TotalPrescriptions { get; set; }



    }
}

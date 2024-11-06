using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.View_Models.Nurse
{
    public class PatientVitalsDetailsViewModel
    {
        public int PatientVitalsDetailsID { get; set; }
        public int VitalID { get; set; }


        public string Description { get; set; }
         
        public int FileID { get; set; }
        public string PatientName { get; set; }

        public string VitalValue { get; set; }


        public DateTime DateRecorded { get; set; }

        public int PatientVitalsID { get; set; }

        public string NurseName { get; set; }   
    }
}

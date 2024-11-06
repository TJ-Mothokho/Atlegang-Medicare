using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.View_Models.Nurse
{
    public class VMPatientTreatment
    {
        public int PatientTreatmentID { get; set; }

        public int FileID { get; set; }

        public string PatientName { get; set; }

        public int TreatmentID { get; set; }

        public string Description { get; set; }


        public DateTime Date { get; set; }

        public DateTime LastTreatmentDate { get; set; }

        public int TotalTreatments { get; set; }


    }
}

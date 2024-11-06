using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.View_Models.Nurse
{
    public class VMPatientVitals
    {
        public int PatientVitalsID { get; set; }

        public int FileID { get; set; }

        public string PatientName { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateRecorded { get; set; }

        public int NurseID { get; set; }

        public string NurseName { get; set; }

        public DateTime LastVitalRecordedDate { get; set; }
        public int TotalVitalsRecorded { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.View_Models.WardAdministror
{
    public class VMPatientMedication
    {
        public int PatientMedsID { get; set; }
        public int PatientID { get; set; }
        public int MedicationID { get; set; }
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Domains.Ward_Administrator
{
    public class PatientMedication
    {
        public int PatientMedsID { get; set; }
        public int PatientID { get; set; }
        public int MedicationID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.View_Models.WardAdministror
{
    public class VMPatientAllergy
    {
        public int PatientAllergyID { get; set; }
        public int PatientID { get; set; }
        public int AllergyID { get; set; }
        public string Description { get; set; }
    }
}

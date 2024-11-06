using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Domains.Ward_Administrator
{
    public class PatientAllergy
    {
        public int PatientAllergyID { get; set; }
        public int PatientID { get; set; }
        public int AllergyID { get; set; }
    }
}

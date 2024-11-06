using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister
{
    public class PatientVitalDetails
    {
        [Key]
        public int PatientVitalDetailID { get; set; }
        public int VitalID { get; set; }

        public string VitalValue { get; set; }
        public int PatientVitalsID { get; set; }

    }
}

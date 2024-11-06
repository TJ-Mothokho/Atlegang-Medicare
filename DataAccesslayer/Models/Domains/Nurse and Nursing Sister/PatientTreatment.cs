using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister
{
    public class PatientTreatment
    {
        public int PatientTreatmentID { get; set; }
        public int FileID { get; set; }
        public int TreatmentID { get; set; }
        //public IEnumerable<Treatment> Treatments { get; set; }
        //public IEnumerable<Note> Notes { get; set; }
        //public IEnumerable<Medication> medications { get; set; }
        public DateTime Date { get; set; }
    }
}

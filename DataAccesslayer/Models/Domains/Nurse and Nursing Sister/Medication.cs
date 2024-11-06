using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister
{
    public class Medication
    {
        public int MedicationID { get; set; }
        public string Name { get; set; }
        public DateTime ExpiryDate { get; set; }

        public int Schedule { get; set; }

        public string Status { get; set; }


    }

}


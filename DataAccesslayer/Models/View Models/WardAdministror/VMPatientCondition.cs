using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.View_Models.WardAdministror
{
    public class VMPatientCondition
    {
        public int PatientConditionID { get; set; }
        public int PatientID { get; set; }
        public int ConditionID { get; set; }
        public string ChronicType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.View_Models.WardAdministror
{
    public class VMPatientMovement
    {
        public int MovementID { get; set; }
        public int FileID { get; set; }
        public string PatientName { get; set; }
        public string Description { get; set; }
        public DateTime Date {  get; set; }
        public string Status { get; set; }
    }   
}

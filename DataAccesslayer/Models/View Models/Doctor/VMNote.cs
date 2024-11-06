using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.View_Models.Doctor
{
    public class VMNote
    {
        public int NoteID { get; set; } 
        public int FileID { get; set; }

        public int PatientID { get; set; }  
        public string PatientName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

    }
}

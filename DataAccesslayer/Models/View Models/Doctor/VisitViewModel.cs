using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.View_Models.Doctor
{
    public class VisitViewModel
    {
        public int VisitID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DoctorID { get; set; }
        public int FileID { get; set; }
        public string PatientName { get; set; }
    }
}

using DataAccesslayer.Models.Domains.Administrator;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Domains.Ward_Administrator
{
    public class PatientFile
    {
        public int FileID { get; set; }
        public int PatientID { get; set; }     
        public int BedID { get; set; }
        public int DoctorID { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string? Status { get; set; }

 
    }
}

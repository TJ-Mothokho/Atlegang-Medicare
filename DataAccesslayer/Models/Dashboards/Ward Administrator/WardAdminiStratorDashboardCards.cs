using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Dashboards.Ward_Administrator
{
    public class WardAdminiStratorDashboardCards
    {
        public int TotalAdmittedPatients { get; set; }
        public int TotalBeds { get; set; }
        public int TotalReferredPatients { get; set; }
        public int TotalOverallPatients { get; set; }
        public int TotalDischargePatients { get; set; } 
        public int TotalAdmittedPatientsPerDay { get; set; }
       public int TotalDischargedPerDay { get; set; }
       public int TotalClosedFiles { get; set; }
        //public int TotalBeds { get; set; }
        //public int TotalWards { get; set; }
        //public int TotalEmployees { get; set; }


    }
}

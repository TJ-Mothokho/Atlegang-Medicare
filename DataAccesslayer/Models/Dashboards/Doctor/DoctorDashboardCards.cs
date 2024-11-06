using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Dashboards.Doctor
{
    public class DoctorDashboardCards
    {
        //Doctor Dashboard Cards
        public int TotalPatientsFiles { get; set; }
        public int TotalVisits { get; set; }
        public int TotalDischargedPatients { get; set; }
        public int TotalReferredPatients { get; set; }
        //Doctor Dashboard Charts


        //Doughnut Chart
        public int TotalMalePatients { get; set;}
        public int TotalFemalePatients { get; set; }
        public int TotalOtherPatients { get; set; }

        //Pie Chart


    }
}

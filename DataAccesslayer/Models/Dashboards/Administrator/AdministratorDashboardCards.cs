using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Dashboards.Administrator
{
    public class AdministratorDashboardCards
    {
        //Administrator Dashboard Cards
        public int TotalPatients { get; set; }
        public int TotalBeds { get; set; }
        public int TotalWards { get; set; }
        public int TotalEmployees { get; set; }

        //Administrator Dashboard Charts (Pie chart)
        public int TotalMaleEmployees { get; set; }
        public int TotalFemaleEmployees { get; set; }
        public int TotalOtherEmployees { get; set; }


        //Administrator Dashboard Chart (line Chart)
        public int TotalMannagers { get; set; }
    }
}

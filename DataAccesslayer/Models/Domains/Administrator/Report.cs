using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Domains.Administrator
{
    public class Report
    {
        public int ReportID { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }
        public DateTime DateOccured { get; set; }
    }
}

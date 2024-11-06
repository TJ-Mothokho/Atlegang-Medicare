using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Domains.Administrator
{
    public class Suburb
    {
        public int SuburbID { get; set; }
        public string SuburbName { get; set; }
        public int CityID { get; set; }
        public int PostalCode { get; set; }
    }
}

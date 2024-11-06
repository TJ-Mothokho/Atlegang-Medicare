using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Domains.Administrator
{
    public class Bed
    {
        public int BedID { get; set; }
        public int RoomID { get; set; }
        public string Status{ get; set; }
    }
}

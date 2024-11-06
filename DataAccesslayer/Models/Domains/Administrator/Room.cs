using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Domains.Administrator
{
    public class Room
    {
        public int RoomID { get; set; }
        public int BedCapacity { get; set; }
        public string Status { get; set; }
        public int WardID { get; set; }
    }
}

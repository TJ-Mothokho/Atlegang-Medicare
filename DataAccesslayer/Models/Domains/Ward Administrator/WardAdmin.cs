using DataAccesslayer.Models.Domains.Administrator;

namespace DataAccesslayer.Models.Domains.Ward_Administrator
{
    public class WardAdmin : User
    {
        public int WardAdminID { get; set; }
        public int  WardID { get; set; }
    }
}

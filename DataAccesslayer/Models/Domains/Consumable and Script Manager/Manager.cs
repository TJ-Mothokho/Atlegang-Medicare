using DataAccesslayer.Models.Domains.Administrator;

namespace DataAccesslayer.Models.Domains.Script_Manager
{
    public class Manager : User
    {
        public int ManagerID { get; set; }
        public string KeyType { get; set; }
    }
}

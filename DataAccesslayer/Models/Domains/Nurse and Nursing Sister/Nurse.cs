using DataAccesslayer.Models.Domains.Administrator;

namespace DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister
{
    public class Nurse : User
    {
        public int NurseID { get; set; }
        public string Type { get; set; }
    }
}

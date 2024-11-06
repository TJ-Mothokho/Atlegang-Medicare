using DataAccesslayer.Models.Domains.Administrator;

namespace DataAccesslayer.Models.Domains.Doctor
{
    public class Patient : User
    {
        public int PatientID { get; set; }
    }
}

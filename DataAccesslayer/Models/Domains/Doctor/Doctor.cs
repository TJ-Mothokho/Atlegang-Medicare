using DataAccesslayer.Models.Domains.Administrator;

namespace DataAccesslayer.Models.Domains.Doctor
{
    public class Doctor : User
    {
        public int DoctorID { get; set; }
        public int SpecialtyID { get; set; }
    }
}

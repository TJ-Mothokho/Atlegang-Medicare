using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.Models.View_Models.Administrator;
using DataAccesslayer.Models.View_Models.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Ward_Aministrator
{
    public interface IDoctorRepository
    {

        Task<IEnumerable<User>> GetAllDoctorsAsync();
        Task<IEnumerable<VMDoctor>> GetAllDoctorsByNameAsync();
        Task<User> GetDoctorById(int UserID);

    }
}

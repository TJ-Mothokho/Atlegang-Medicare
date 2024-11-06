using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.Models.View_Models.Administrator;
using DataAccesslayer.Models.View_Models.Doctor;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Ward_Aministrator
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ISqlDataAccess _data;

        public DoctorRepository(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<IEnumerable<User>> GetAllDoctorsAsync()
        {
            return await _data.GetData<User, dynamic>("spGetAllDoctors", new { });
        }

        public async Task<IEnumerable<VMDoctor>> GetAllDoctorsByNameAsync()
        {
            return await _data.GetData<VMDoctor, dynamic>("spGetDoctorsByName", new { });
        }

        public async Task<User> GetDoctorById(int UserID)
        {
            var doctors = await _data.GetData<User, dynamic>("spGetDoctorByID", new { UserID});
            return doctors.FirstOrDefault();
        }
    }
}

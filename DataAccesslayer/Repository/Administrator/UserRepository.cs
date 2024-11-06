using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Models.View_Models.Administrator;
using DataAccesslayer.SqlDataAccess;

namespace DataAccesslayer.Repository.Administrator
{
    public class UserRepository : IUserRepository
    {
        private readonly ISqlDataAccess _data;

        public UserRepository(ISqlDataAccess data)
        {
            _data = data;
        }

       
      
        //public async Task<bool> AddPatientAsync(Patient user)
        //{
        //    try
        //    {
        //        await _data.SaveData("spInsertPatient", new { user.UserID, user.Gender, user.SuburbID, user.DateOfBirth, user.IDNumber, user.ResidentialAddress, user.PhoneNumbers });
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public async Task<bool> AddEmployeeAsync(User user)
        {
            try
            {
                await _data.SaveData("spAddEmployee", new { user.LastName, user.FirstName, user.IDNumber, user.Gender, user.Email, user.Password, user.PhoneNumbers, user.RoleID, user.DateOfBirth, user.ResidentialAddress, user.SuburbID });
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<dynamic>> GetAllEmployeesAsync()
        {
            return await _data.GetData<dynamic, dynamic>("spGetAllEmployees", new { });
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var result = await _data.GetData<User, dynamic>("spGetUserById", new { userId });
            return result.FirstOrDefault();
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _data.GetData<User, dynamic>("spLogin", new { email, password});

            return user.FirstOrDefault();
        }

        public async Task<bool> UpdateEmployeeAsync(User user)
        {
            try
            {
                await _data.SaveData("spUpdateEmployee", new { user.UserID, user.FirstName, user.LastName, user.PhoneNumbers, user.ResidentialAddress, user.SuburbID, user.Gender });
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }


        public async Task<bool> DeleteUserAsync(int userId)
        {
            try
            {
                await _data.SaveData("spDeleteUser", new { userId });
                return true;
            }
            catch (System.Exception)
            {
                return false;
            } 
        }

        public async Task<User> GetUserTestByIdAsync(int userId)
        {
            var result = await _data.GetData<User, dynamic>("spGetUserTestById", new { userId });
            return result.FirstOrDefault();
        }
        public async Task<VMUser> GetUserDetails(int userId)
        {
            var result = await _data.GetData<VMUser, dynamic>("spGetUserDetails", new { userId });
            return result.FirstOrDefault();
        }

    }
}

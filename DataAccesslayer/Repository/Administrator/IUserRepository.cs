using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Models.View_Models.Administrator;

namespace DataAccesslayer.Repository.Administrator
{
    public interface IUserRepository
    {
        Task<User> Login(string email, string password);
        Task<IEnumerable<dynamic>> GetAllEmployeesAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<bool> AddEmployeeAsync(User user);
        Task<bool> UpdateEmployeeAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<VMUser> GetUserDetails(int userID);
        Task<User> GetUserTestByIdAsync(int userId);
    }
}

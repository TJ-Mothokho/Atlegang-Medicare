using AdministratorServices.Domain.Entities;

namespace AdministratorServices.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> LoginAsync(string email, string password);
        Task<IEnumerable<User>> GetAllAsync(string role);
        Task<IEnumerable<Doctor>> GetDoctorBySpecialty(Guid id);
        Task<User> GetByIdAsync(Guid id);
        Task<bool> AddAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid id);
    }
}

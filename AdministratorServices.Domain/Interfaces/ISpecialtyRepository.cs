using AdministratorServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministratorServices.Domain.Interfaces
{
    public interface ISpecialtyRepository
    {
        Task<IEnumerable<Specialty>> GetAllAsync();
        Task<Specialty> GetSpecialtyById(Guid id);
        Task<bool> AddSpecialtyAsync(Specialty specialty);
        Task<bool> UpdateSpecialtyAsync(Specialty specialty);
        Task<bool> DeleteSpecialtyAsync(Guid id);
    }
}

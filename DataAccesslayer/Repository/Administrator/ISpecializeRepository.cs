using DataAccesslayer.Models.Domains.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Administrator
{
    public interface ISpecializeRepository
    {
        Task<bool> AddSpecializeAsync(Specialize specialize);
        Task<bool> UpdateSpecializeAsync(Specialize specialize);
        Task<Specialize> GetSpecializeByIdAsync(int SpecializeID);
        Task<IEnumerable<dynamic>> GetAllSpecializesAsync();
    }
}

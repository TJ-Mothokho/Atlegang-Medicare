using DataAccesslayer.Models.Domains.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Administrator
{
    public interface IRoleRepository
    {
        Task<bool> AddRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(int RoleID);
        Task<bool> UpdateRoleAsync(Role role);
        Task<Role> GetRoleByIdAsync(int RoleID);
        Task<IEnumerable<Role>> GetAllRolesAsync();
    }
}

using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Administrator
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ISqlDataAccess _db;

        public RoleRepository(ISqlDataAccess db)
        {
            _db = db;
        }
        public async Task<bool> AddRoleAsync(Role role)
        {
            try
            {
                await _db.SaveData("spInsertRole", new { role.RoleName });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteRoleAsync(int RoleID)
        {
            try
            {
                await _db.SaveData("spDeleteRole", new { RoleID });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _db.GetData<Role, dynamic>("spGetAllRoles", new { });
        }

        public async Task<Role> GetRoleByIdAsync(int RoleID)
        {
            var result = await _db.GetData<Role, dynamic>("spGetRoleById", new { RoleID });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateRoleAsync(Role role)
        {
            try
            {
                await _db.SaveData("spUpdateRole", new { role.RoleID, role.RoleName });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

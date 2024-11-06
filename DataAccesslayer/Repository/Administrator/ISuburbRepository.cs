using DataAccesslayer.Models.Domains.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Administrator
{
    public interface ISuburbRepository
    {
        Task<bool> AddSuburbAsync(Suburb suburb);
        Task<bool> UpdateSuburbAsync(Suburb suburb);
        Task<Suburb> GetSuburbByIdAsync(int SuburbID);
        Task<IEnumerable<Suburb>> GetAllSuburbsAsync();
    }
}

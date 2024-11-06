using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.SqlDataAccess;

namespace DataAccesslayer.Repository.Administrator
{
    public class BusinessInformationRepository : IBusinessInformationRepository
    {
        private readonly ISqlDataAccess _data;

        public BusinessInformationRepository(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<BusinessInformation> GetBusinessInformationAsync()
        {
            var result = await _data.GetData<BusinessInformation, dynamic>("spGetBusinessInformation", new { });
            return result.FirstOrDefault();
        }

        public async Task<BusinessInformation> GetInvoiceInformationAsync()
        {
            var result = await _data.GetData<BusinessInformation, dynamic>("spGetInvoiceInformation", new { });
            return result.FirstOrDefault();
        }
    }
}

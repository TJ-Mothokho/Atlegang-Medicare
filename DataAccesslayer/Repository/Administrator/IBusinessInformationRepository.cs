using DataAccesslayer.Models.Domains.Administrator;

namespace DataAccesslayer.Repository.Administrator
{
    public interface IBusinessInformationRepository
    {
        Task<BusinessInformation> GetBusinessInformationAsync();
        Task<BusinessInformation> GetInvoiceInformationAsync();
    }
}

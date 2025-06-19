

using AdministratorServices.Domain.Entities;

namespace AdministratorServices.Domain.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
        Task<Country> GetCountryById(Guid id);
        Task<bool> AddCountryAsync(Country country);
        Task<bool> DeleteContryAsync(Guid id);


        Task<IEnumerable<Province>> GetAllProvincesAsync(string country);
        Task<Province> GetProvinceById(Guid id);
        Task<bool> AddProvinceAsync(Province province);
        Task<bool> DeleteProvince(Guid id);


        Task<IEnumerable<City>> GetAllCitiesAsync(string province);
        Task<City> GetCityByIdAsync(Guid id);
        Task<bool> AddCityAsync(City city);
        Task<bool> UpdateCityAsync(City city);
        Task<bool> DeleteCityAsync(Guid id);


        Task<IEnumerable<Surburb>> GetAllSurburbsAsync(string city);
        Task<IEnumerable<Surburb>> GetSurburbsByPostalCodeAsync(string postalCode);
        Task<Surburb> GetSurburbByIdAsync(Guid id);
        Task<bool> AddSurburbAsync(Surburb surburb);
        Task<bool> UpdateSurburbAsync(Surburb surburb);
        Task<bool> DeleteSurburbAsync(Guid id);

    }
}

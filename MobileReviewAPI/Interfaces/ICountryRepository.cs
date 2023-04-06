using MobileReviewAPI.Models;

namespace MobileReviewAPI.Interfaces
{
    public interface ICountryRepository
    {
        //Get Section
        Task<IEnumerable<Country>> GetAllCountriesAsync();
        Task<Country> GetCountryById(int id);
        Task<Country> GetCountryByOwner(int id);
        Task<IEnumerable<Owner>> GetOwnersFromCountry(int id);
        bool CountryExists(int id);
        //Create Section
        Task<bool> CreateCountry(Country country);
        Task<bool> Save();

        //Update
        Task<bool> UpdateCountry(Country country);
        //Delete
        Task<bool> DeleteCountry(Country country);



    }
}

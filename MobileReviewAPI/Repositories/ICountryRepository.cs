using MobileReviewAPI.Models;

namespace MobileReviewAPI.Repositories
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllCountries();
        Task<Country>GetCountryById(int id);
        Task<Country> GetCountryByOwner(int id);
        Task<IEnumerable<Owner>> GetOwnersFromCountry(int id);
        bool CountryExists(int id);



    }
}

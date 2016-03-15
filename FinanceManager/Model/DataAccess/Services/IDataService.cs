using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManager.Model.DataAccess.Services
{
    /// <summary>
    /// The data service interface
    /// </summary>
    public interface IDataService<T> where T : class, new()
    {
        // Get all items of type T
        List<T> GetAll();

        // Get an item of type T by id
        T Get(int id);

        // Create an item of type T
        void Create(T item);

        // Update an item of type T
        void Update(T item);

        // Delete an item of type T by id
        void Delete(int id);

        // Delete all items of type T
        void DeleteAll();

        // Asynchronously get all items of type T
        Task<List<T>> GetAllAsync();

        // Asynchronously get an item of type T by id
        Task<T> GetAsync(int id);

        // Asynchronously create an item of type T
        Task CreateAsync(T item);

        // Asynchronously update an item of type T
        Task UpdateAsync(T item);

        // Asynchronously delete an item of type T by id
        Task DeleteAsync(int id);

        // Asynchronously delete all items of type T
        Task DeleteAllAsync();
    }
}

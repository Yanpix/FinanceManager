using FinanceManager.Model.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Model.DataAccess.Services
{
    /// <summary>
    /// Generic data service
    /// </summary>
    public class DataService<T> : IDataService<T> where T : class, new()
    {
        // Unit of work declaration
        public IUnitOfWork UnitOfWork { get; set; }

        // Create an item of type T
        public void Create(T item)
        {
            UnitOfWork.Repository<T>().Create(item);
        }

        // Asynchronously create an item of type T
        public async Task CreateAsync(T item)
        {
            await UnitOfWork.Repository<T>().CreateAsync(item);
        }

        // Delete an item of type T by id
        public void Delete(int id)
        {
            UnitOfWork.Repository<T>().Delete(id);
        }

        // Delete all items of type T
        public void DeleteAll()
        {
            UnitOfWork.Repository<T>().DeleteAll();
        }

        // Asynchronously delete all items of type T
        public async Task DeleteAllAsync()
        {
            await UnitOfWork.Repository<T>().DeleteAllAsync();
        }

        // Asynchronously delete an item of type T by id
        public async Task DeleteAsync(int id)
        {
            await UnitOfWork.Repository<T>().DeleteAsync(id);
        }

        // Get an item of type T by id
        public T Get(int id)
        {
            return UnitOfWork.Repository<T>().Get(id);
        }

        // Get all items of type T
        public List<T> GetAll()
        {
            return UnitOfWork.Repository<T>().GetAll();
        }

        // Asynchronously get all items of type T
        public async Task<List<T>> GetAllAsync()
        {
            return await UnitOfWork.Repository<T>().GetAllAsync();
        }

        // Asynchronously get an item of type T by id
        public async Task<T> GetAsync(int id)
        {
            return await UnitOfWork.Repository<T>().GetAsync(id);
        }

        // Update an item of type T
        public void Update(T item)
        {
            UnitOfWork.Repository<T>().Update(item);
        }

        // Asynchronously update an item of type T
        public async Task UpdateAsync(T item)
        {
            await UnitOfWork.Repository<T>().UpdateAsync(item);
        }
    }
}

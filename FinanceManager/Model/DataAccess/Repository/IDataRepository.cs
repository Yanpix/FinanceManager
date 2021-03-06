﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManager.Model.DataAccess.Repository
{
    /// <summary>
    /// The data repository interface
    /// </summary>
    public interface IDataRepository<T> where T : class, new()
    {
        // Get all items of type T
        List<T> GetAll();

        // Get an item of type T by id
        T Get(int id);

        // Create an item of type T
        void Save(T item);

        // Update an item of type T
        void Update(T item);

        // Delete an item of type T by id
        void Delete(int id);

        // Delete all items of type T
        void DeleteAll();
    }
}

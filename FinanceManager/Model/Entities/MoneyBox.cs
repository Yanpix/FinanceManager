using FinanceManager.Dependencies;
using System;

namespace FinanceManager.Model.Entities
{
    /// <summary>
    /// The money box
    /// </summary>
    public class MoneyBox
    {
        // The primary key of the money box
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // The title of the money box
        public string Title { get; set; }

        // The creation date and time of the money box
        public DateTime CreationDate { get; set; }

        // The balance of the money box
        public decimal Balance { get; set; }
    }
}

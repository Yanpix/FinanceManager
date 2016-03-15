using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
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

        // The date and time of last modification
        public DateTime LastModifiedDate { get; set; }

        // The balance of the money box
        public decimal Balance { get; set; }

        // The primary currency id of the money box
        [ForeignKey(typeof(Currency))]
        public int PrimaryCurrencyId { get; set; }

        // The primary currency of the money box
        [ManyToOne]
        public Currency PrimaryCurrency { get; set; }
    }
}

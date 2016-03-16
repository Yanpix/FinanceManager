using FinanceManager.Model.Entities.Enums;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;

namespace FinanceManager.Model.Entities
{
    /// <summary>
    /// The transaction
    /// </summary>
    public class Transaction
    {
        // The primary key of the transaction
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // The creation date and time of the transaction
        public DateTime CreationDate { get; set; }

        // The value of the transaction
        public decimal Value { get; set; }

        // The transaction type, can be income or expence
        public TransactionType Type { get; set; }

        // The user note for the transaction
        public string Note { get; set; }

        // The currency id of the transaction
        [ForeignKey(typeof(Currency))]
        public int CurrencyId { get; set; }

        // The currency of the transaction
        [ManyToOne]
        public Currency Currency { get; set; }

        // The money box id of the transaction
        [ForeignKey(typeof(MoneyBox))]
        public int MoneyBoxId { get; set; }

        // The money box of the transaction
        [ManyToOne]
        public MoneyBox MoneyBox { get; set; }

        // The category id of the transaction
        [ForeignKey(typeof(Category))]
        public int CategoryId { get; set; }

        // The category of the transaction
        [ManyToOne]
        public Category Category { get; set; }
    }
}

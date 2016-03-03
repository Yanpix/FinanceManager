using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;

namespace FinanceManager.DAL.Models
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        [ForeignKey(typeof(User))]
        public int CreatedBy { get; set; }

        public decimal Value { get; set; }

        public TransactionType Type { get; set; }

        public string Note { get; set; }

        public decimal ToPrimaryKoeff { get; set; }

        [ForeignKey(typeof(MoneyBox))]
        public int MoneyBoxId { get; set; }

        [ForeignKey(typeof(Category))]
        public int CategoryId { get; set; }

        [ForeignKey(typeof(Currency))]
        public int CurrencyId { get; set; }

        [ManyToOne]
        public User User { get; set; }

        [ManyToOne]
        public MoneyBox MoneyBox { get; set; }

        [ManyToOne]
        public Category Category { get; set; }

        [ManyToOne]
        public Currency Currency { get; set; }
    }
}

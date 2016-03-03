using System;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using SQLite.Net.Attributes;

namespace FinanceManager.DAL.Models
{
    public class MoneyBox
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public decimal Balance { get; set; }

        [ForeignKey(typeof(Currency))]
        public int PrimaryCurrencyId { get; set; }

        [OneToMany]
        public List<Transaction> Transactions { get; set; }

        [OneToMany]
        public List<User> Users { get; set; }

        [ManyToOne]
        public Currency PrimaryCurrency { get; set; }
    }
}

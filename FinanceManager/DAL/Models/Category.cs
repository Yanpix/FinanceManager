using SQLite.Net.Attributes;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace FinanceManager.DAL.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public TransactionType Type { get; set; }

        [OneToMany]
        public List<Transaction> Transactions { get; set; }
    }
}

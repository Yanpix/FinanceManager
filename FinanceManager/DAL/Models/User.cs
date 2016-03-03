using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace FinanceManager.DAL.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [ForeignKey(typeof(MoneyBox))]
        public int MoneyBoxId { get; set; }

        [OneToMany]
        public List<Transaction> Transactions { get; set; }

        [ManyToOne]
        public MoneyBox MoneyBox { get; set; }
    }
}

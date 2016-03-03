using SQLite.Net.Attributes;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace FinanceManager.DAL.Models
{
    public class Currency
    {
        [PrimaryKey, AutoIncrement]
        public short Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }  

        [OneToMany]
        public List<MoneyBox> MoneyBoxes { get; set; }

        [OneToMany]
        public List<Transaction> Transactions { get; set; }
    }
}

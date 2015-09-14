using System;
using FinanceManager.DAL.SQLite;

namespace FinanceManager.DAL.Models
{
    public class MoneyBox : BaseModel
    {
        [PrimaryKey]
        public DateTime Date { get; set; }
        public Income Income { get; set; }
        public Expense Expense { get; set; }
    }
}

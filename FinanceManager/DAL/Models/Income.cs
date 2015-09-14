using System;
using FinanceManager.DAL.SQLite;

namespace FinanceManager.DAL.Models
{
    public class Income : BaseModel
    {
        [PrimaryKey]
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public IncomeCategory Category { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
    }
}

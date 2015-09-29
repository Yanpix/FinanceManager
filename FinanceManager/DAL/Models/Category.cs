using System;
using FinanceManager.DAL.SQLite;

namespace FinanceManager.DAL.Models
{
    public class Category
    {
        [AutoIncrement]
        [PrimaryKey]
        public short CategoryId { get; set; }
        public CategoryTypeEnum CategoryType { get; set; }
        public string Name { get; set; }

    }

    public enum CategoryTypeEnum
    {
        Income,
        Expense
    }
}

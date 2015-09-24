using FinanceManager.DAL.SQLite;

namespace FinanceManager.DAL.Models
{
    public class ExpenseCategory : BaseModel
    {
        [PrimaryKey]
        public short ExpenseCategoryId { get; set; }
        public string Name { get; set; }
    }
}

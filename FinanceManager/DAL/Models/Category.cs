using FinanceManager.DAL.SQLite;

namespace FinanceManager.DAL.Models
{
    public class ExpenseCategory
    {
        [PrimaryKey]
        public short Id { get; set; }
        public string Name { get; set; }
    }
}

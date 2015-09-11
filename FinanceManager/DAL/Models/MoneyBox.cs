namespace FinanceManager.DAL.Models
{
    public class MoneyBox : BaseModel
    {
        public Income Income { get; set; }
        public Expense Expense { get; set; }
    }
}

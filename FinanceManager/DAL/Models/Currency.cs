using FinanceManager.DAL.SQLite;

namespace FinanceManager.DAL.Models
{
    public class Currency : BaseModel
    {
        public short CurrencyId { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
    }
}

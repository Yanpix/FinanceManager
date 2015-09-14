using FinanceManager.DAL.SQLite;

namespace FinanceManager.DAL.Models
{
    public class Currency : BaseModel
    {
        [PrimaryKey]
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}

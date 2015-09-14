using FinanceManager.DAL.SQLite;

namespace FinanceManager.DAL.Models
{
    public class Currencies : BaseModel
    {
        [PrimaryKey]
        public Currency Currency { get; set; }
    }
}

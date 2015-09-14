using FinanceManager.DAL.SQLite;

namespace FinanceManager.DAL.Models
{
    public class BaseModel
    {
        [AutoIncrement]
        public int Id { get; set; }
    }
}

namespace FinanceManager.DAL.Models
{
    public class BaseModel
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
    }
}

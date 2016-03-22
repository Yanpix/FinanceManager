using FinanceManager.Model.Entities.Enums;
using SQLite.Net.Attributes;

namespace FinanceManager.Model.Entities
{
    /// <summary>
    /// The category of transaction
    /// </summary>
    public class Category
    {
        // The primary key of the category
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // The title of the category
        public string Title { get; set; }

        // The transaction type of the category, can be income or expence
        public TransactionType Type { get; set; }
    }
}

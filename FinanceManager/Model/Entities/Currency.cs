using FinanceManager.Dependencies;

namespace FinanceManager.Model.Entities
{
    /// <summary>
    /// The currency
    /// </summary>
    public class Currency
    {
        // The primary key of the currency
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // The title of the currency
        public string Title { get; set; }

        // The symbol of the currency
        public string Symbol { get; set; }
    }
}

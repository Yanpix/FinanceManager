using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using YanpixFinanceManager.Model.Entities.Common;

namespace YanpixFinanceManager.Model.Entities
{
    [Table("MoneyBox")]
    public class MoneyBox : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50), Unique]
        public string Title { get; set; }

        public decimal YearBudget { get; set; }

        public decimal MonthBudget { get; set; }

        #region Navigation Properties

        [ForeignKey(typeof(Currency))]
        public int PrimaryCurrencyId { get; set; }

        [ManyToOne]
        public Currency PrimaryCurrency { get; set; }

        #endregion
    }
}

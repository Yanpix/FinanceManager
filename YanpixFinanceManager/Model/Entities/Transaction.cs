using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using YanpixFinanceManager.Model.Entities.Common;
using YanpixFinanceManager.Model.Entities.Enums;

namespace YanpixFinanceManager.Model.Entities
{
    [Table("Transaction")]
    public class Transaction : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public decimal Value { get; set; }

        public TransactionType Type { get; set; }

        [MaxLength(200)]
        public string Note { get; set; }

        public decimal ToPrimaryCoeff { get; set; }

        #region Navigation Properties

        [ForeignKey(typeof(Currency))]
        public int CurrencyId { get; set; }

        [ManyToOne]
        public Currency Currency { get; set; }

        [ForeignKey(typeof(Category))]
        public int CategoryId { get; set; }

        [ManyToOne]
        public Category Category { get; set; }

        [ForeignKey(typeof(ReportingPeriod))]
        public int ReportingPeriodId { get; set; }

        [ManyToOne]
        public ReportingPeriod ReportingPeriod { get; set; }

        #endregion
    }
}

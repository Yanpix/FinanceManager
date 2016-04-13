using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using YanpixFinanceManager.Model.Entities.Common;
using YanpixFinanceManager.Model.Entities.Enums;

namespace YanpixFinanceManager.Model.Entities
{
    [Table("ReportingPeriod")]
    public class ReportingPeriod : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public ReportingPeriodType Type { get; set; }

        public DateTime Period { get; set; }

        public decimal Income { get; set; }

        public decimal Expence { get; set; }

        public decimal Budget { get; set; }

        #region Calculated Properties

        [Ignore]
        public decimal Balance
        {
            get { return Income - Expence; }
        }

        [Ignore]
        public decimal BudgetBalance
        {
            get { return Budget - Expence; }
        }

        #endregion

        #region Navigation Properties

        [ForeignKey(typeof(MoneyBox))]
        public int MoneyBoxId { get; set; }

        [ManyToOne]
        public MoneyBox MoneyBox { get; set; }

        [ForeignKey(typeof(ReportingPeriod))]
        public int ParentPeriodId { get; set; }

        [ManyToOne]
        public ReportingPeriod ParentPeriod { get; set; }

        #endregion
    }
}

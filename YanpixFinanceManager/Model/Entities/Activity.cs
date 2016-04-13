using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using YanpixFinanceManager.Model.Entities.Common;
using YanpixFinanceManager.Model.Entities.Enums;

namespace YanpixFinanceManager.Model.Entities
{
    [Table("Activity")]
    public class Activity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public ActivityType Type { get; set; }

        [MaxLength(30)]
        public string Property { get; set; }

        [MaxLength(200)]
        public string OldValue { get; set; }

        [MaxLength(200)]
        public string NewValue { get; set; }

        #region Navigation Properies

        [ForeignKey(typeof(IEntityBase))]
        public int EntityId { get; set; }

        [ManyToOne]
        public IEntityBase Entity { get; set; }

        [ForeignKey(typeof(IEntityBase))]
        public int AccountId { get; set; }

        [ManyToOne]
        public Account Account { get; set; }

        #endregion
    }
}

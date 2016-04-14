using SQLite.Net.Attributes;
using YanpixFinanceManager.Model.Entities.Common;

namespace YanpixFinanceManager.Model.Entities
{
    [Table("Settings")]
    public class Setting : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public bool TransferBalance { get; set; }

        public int PeriodStartDay { get; set; }
    }
}

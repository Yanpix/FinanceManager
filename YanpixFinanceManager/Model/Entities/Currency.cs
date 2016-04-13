using SQLite.Net.Attributes;
using YanpixFinanceManager.Model.Entities.Common;
using YanpixFinanceManager.Model.Entities.Enums;

namespace YanpixFinanceManager.Model.Entities
{
    [Table("Currency")]
    public class Currency : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(3), Unique]
        public CurrencyType Abbreviation { get; set; }

        [MaxLength(30), Unique]
        public string Title { get; set; }

        [MaxLength(1)]
        public string Symbol { get; set; }

        [MaxLength(50)]
        public string Image { get; set; }
    }
}

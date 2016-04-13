using SQLite.Net.Attributes;
using YanpixFinanceManager.Model.Entities.Common;
using YanpixFinanceManager.Model.Entities.Enums;

namespace YanpixFinanceManager.Model.Entities
{
    [Table("Category")]
    public class Category : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Title { get; set; }

        public TransactionType Type { get; set; }

        [MaxLength(50)]
        public string Image { get; set; }
    }
}

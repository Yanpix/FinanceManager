using SQLite.Net.Attributes;
using YanpixFinanceManager.Model.Entities.Common;

namespace YanpixFinanceManager.Model.Entities
{
    [Table("Account")]
    public class Account : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50), Unique]
        public string Username { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }
    }
}

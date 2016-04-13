using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YanpixFinanceManager.Model.Entities.Common;

namespace YanpixFinanceManager.Model.Entities
{
    [Table("CurrencyExchange")]
    public class CurrencyExchange : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(3)]
        public string From { get; set; }

        [MaxLength(3)]
        public string To { get; set; }

        public decimal Value { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}

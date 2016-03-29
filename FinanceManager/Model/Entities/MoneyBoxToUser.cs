using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Model.Entities
{
    /// <summary>
    /// Intermediate entity for relationship between money boxes and users
    /// </summary>
    public class MoneyBoxToUser
    {
        // Primary key
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // The id of related money box
        [ForeignKey(typeof(MoneyBox))]
        public int MoneyBoxId { get; set; }

        // The id of related user
        [ForeignKey(typeof(User))]
        public int UserId { get; set; }

        // Specifies whether the user is owner or not
        public bool IsOwner { get; set; }

        // Specifies whether user can modify money box or not
        public bool CanModify { get; set; }

        // Specifies whether user can realize transactions or not
        public bool CanRealize { get; set; }
    }
}

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
        // The id of related money box
        [ForeignKey(typeof(MoneyBox))]
        public int MoneyBoxId { get; set; }

        // The id of related user
        [ForeignKey(typeof(User))]
        public int UserId { get; set; }
    }
}

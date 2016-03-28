using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace FinanceManager.Model.Entities
{
    /// <summary>
    /// The user
    /// </summary>
    public class User
    {
        // The primary key of the user, can not be changed by user
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // The title of the user
        public string Title { get; set; }

        // The password of the user
        public string Password { get; set; }

        // Money boxes of the user
        [ManyToMany(typeof(MoneyBoxToUser))]
        public List<MoneyBox> MoneyBoxes { get; set; }
    }
}

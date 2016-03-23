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

        // The first name of the user
        public string FirstName { get; set; }

        // The last name of the user
        public string LastName { get; set; }

        // Money boxes of the user
        [ManyToMany(typeof(MoneyBoxToUser))]
        public List<MoneyBox> MoneyBoxes { get; set; }
    }
}

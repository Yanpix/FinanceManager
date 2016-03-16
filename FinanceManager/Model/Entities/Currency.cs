﻿using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace FinanceManager.Model.Entities
{
    /// <summary>
    /// The currency
    /// </summary>
    public class Currency
    {
        // The primary key of the currency
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // The title of the currency
        public string Title { get; set; }

        // The symbol of the currency
        public string Symbol { get; set; }

        // List of money boxes, where this currency is primary
        [OneToMany]
        public List<MoneyBox> MoneyBoxes { get; set; }

        // List of transactions with this currency
        [OneToMany]
        public List<Transaction> Transactions { get; set; }
    }
}

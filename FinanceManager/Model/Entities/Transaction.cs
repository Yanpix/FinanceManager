using FinanceManager.Model.Entities.Enums;
using FinanceManager.Dependencies;
using System;

namespace FinanceManager.Model.Entities
{
    /// <summary>
    /// The transaction
    /// </summary>
    public class Transaction
    {
        // The primary key of the transaction
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // The creation date and time of the transaction
        public DateTime CreationDate { get; set; }

        // The value of the transaction
        public decimal Value { get; set; }

        // The transaction type, can be income or expence
        public TransactionType Type { get; set; }

        // The user note for the transaction
        public string Note { get; set; }

        // The reduction coefficient to the primary currency
        public decimal ToPrimaryCoeff { get; set; }
    }
}

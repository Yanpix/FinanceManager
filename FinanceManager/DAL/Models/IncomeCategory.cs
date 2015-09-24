using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.DAL.SQLite;

namespace FinanceManager.DAL.Models
{
    public class IncomeCategory : BaseModel
    {
        [PrimaryKey]
        public short IncomeCategoryId { get; set; }
        public string Name { get; set; }
    }
}

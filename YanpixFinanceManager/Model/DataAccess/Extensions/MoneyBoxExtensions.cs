using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YanpixFinanceManager.Model.DataAccess.Services;
using YanpixFinanceManager.Model.Entities;
using YanpixFinanceManager.Model.Entities.Enums;

namespace YanpixFinanceManager.Model.DataAccess.Extensions
{
    public static class MoneyBoxExtensions
    {
        public static decimal GetIncomeForLastMonth(this IEntityBaseService<ReportingPeriod> reportingPeriodService, int moneyBoxId)
        {
            return reportingPeriodService.GetAll()
                        .Where(r => r.MoneyBoxId == moneyBoxId &&
                            r.Type == ReportingPeriodType.Month)
                        .OrderBy(o => o.Period)
                        .Select(s => s.Income)
                        .FirstOrDefault();
        }

        public static decimal GetExpenceForLastMonth(this IEntityBaseService<ReportingPeriod> reportingPeriodService, int moneyBoxId)
        {
            return reportingPeriodService.GetAll()
                        .Where(r => r.MoneyBoxId == moneyBoxId &&
                            r.Type == ReportingPeriodType.Month)
                        .OrderBy(o => o.Period)
                        .Select(s => s.Expence)
                        .FirstOrDefault();
        }

        public static decimal GetBalanceForLastMonth(this IEntityBaseService<ReportingPeriod> reportingPeriodService, int moneyBoxId)
        {
            return reportingPeriodService.GetAll()
                        .Where(r => r.MoneyBoxId == moneyBoxId &&
                            r.Type == ReportingPeriodType.Month)
                        .OrderBy(o => o.Period)
                        .Select(s => s.Balance)
                        .FirstOrDefault();
        }

        public static decimal GetBudgetForLastMonth(this IEntityBaseService<ReportingPeriod> reportingPeriodService, int moneyBoxId)
        {
            return reportingPeriodService.GetAll()
                        .Where(r => r.MoneyBoxId == moneyBoxId &&
                            r.Type == ReportingPeriodType.Month)
                        .OrderBy(o => o.Period)
                        .Select(s => s.Budget)
                        .FirstOrDefault();
        }

        public static decimal GetBudgetBalanceForLastMonth(this IEntityBaseService<ReportingPeriod> reportingPeriodService, int moneyBoxId)
        {
            return reportingPeriodService.GetAll()
                        .Where(r => r.MoneyBoxId == moneyBoxId &&
                            r.Type == ReportingPeriodType.Month)
                        .OrderBy(o => o.Period)
                        .Select(s => s.BudgetBalance)
                        .FirstOrDefault();
        }

        public static MoneyBox ExistingMoneyBox(this IEntityBaseService<MoneyBox> moneyBoxService, string title, int primaryCurrencyId)
        {
            return moneyBoxService.GetAll()
                .Where(m => m.Title == title && m.PrimaryCurrencyId == primaryCurrencyId)
                .FirstOrDefault();
        }
    }
}

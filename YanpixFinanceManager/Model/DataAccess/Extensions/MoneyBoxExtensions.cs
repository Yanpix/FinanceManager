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
                        .LastOrDefault();
        }

        public static decimal GetExpenceForLastMonth(this IEntityBaseService<ReportingPeriod> reportingPeriodService, int moneyBoxId)
        {
            return reportingPeriodService.GetAll()
                        .Where(r => r.MoneyBoxId == moneyBoxId &&
                            r.Type == ReportingPeriodType.Month)
                        .OrderBy(o => o.Period)
                        .Select(s => s.Expence)
                        .LastOrDefault();
        }

        public static decimal GetBalanceForLastMonth(this IEntityBaseService<ReportingPeriod> reportingPeriodService, int moneyBoxId)
        {
            return reportingPeriodService.GetAll()
                        .Where(r => r.MoneyBoxId == moneyBoxId &&
                            r.Type == ReportingPeriodType.Month)
                        .OrderBy(o => o.Period)
                        .Select(s => s.Balance)
                        .LastOrDefault();
        }

        public static decimal GetBudgetForLastMonth(this IEntityBaseService<ReportingPeriod> reportingPeriodService, int moneyBoxId)
        {
            return reportingPeriodService.GetAll()
                        .Where(r => r.MoneyBoxId == moneyBoxId &&
                            r.Type == ReportingPeriodType.Month)
                        .OrderBy(o => o.Period)
                        .Select(s => s.Budget)
                        .LastOrDefault();
        }

        public static decimal GetBudgetBalanceForLastMonth(this IEntityBaseService<ReportingPeriod> reportingPeriodService, int moneyBoxId)
        {
            return reportingPeriodService.GetAll()
                        .Where(r => r.MoneyBoxId == moneyBoxId &&
                            r.Type == ReportingPeriodType.Month)
                        .OrderBy(o => o.Period)
                        .Select(s => s.BudgetBalance)
                        .LastOrDefault();
        }

        public static MoneyBox ExistingMoneyBox(this IEntityBaseService<MoneyBox> moneyBoxService, string title, int primaryCurrencyId)
        {
            return moneyBoxService.GetAll()
                .Where(m => m.Title == title && m.PrimaryCurrencyId == primaryCurrencyId)
                .FirstOrDefault();
        }

        public static ReportingPeriod GetLastYearPeriod(this IEntityBaseService<ReportingPeriod> reportingPeriodService, int moneyBoxId)
        {
            return reportingPeriodService.GetAll()
                    .Where(x => x.MoneyBoxId == moneyBoxId && x.Type == ReportingPeriodType.Year)
                    .OrderBy(o => o.Period)
                    .Last();
        }

        public static ReportingPeriod GetLastMonthPeriod(this IEntityBaseService<ReportingPeriod> reportingPeriodService, int moneyBoxId)
        {
            return reportingPeriodService.GetAll()
                    .Where(x => x.MoneyBoxId == moneyBoxId && x.Type == ReportingPeriodType.Month)
                    .OrderBy(o => o.Period)
                    .Last();
        }
    }
}

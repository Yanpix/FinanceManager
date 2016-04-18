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

        public static decimal GetIncomeForSpecPeriod(this IEntityBaseService<ReportingPeriod> reportingPeriodService, int moneyBoxId, DateTime period, bool isMonthPeriod)
        {
            if (isMonthPeriod)
            {
                return reportingPeriodService.GetAll()
                        .Where(r => r.MoneyBoxId == moneyBoxId &&
                            r.Type == ReportingPeriodType.Month &&
                            r.Period.Year == period.Year &&
                            r.Period.Month == period.Month)
                        .Select(s => s.Income)
                        .SingleOrDefault();
            }
            else
            {
                return reportingPeriodService.GetAll()
                        .Where(r => r.MoneyBoxId == moneyBoxId &&
                            r.Type == ReportingPeriodType.Month &&
                            r.Period.Year == period.Year)
                        .Sum(s => s.Income);
            }
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

        public static decimal GetExpenceForSpecPeriod(this IEntityBaseService<ReportingPeriod> reportingPeriodService, int moneyBoxId, DateTime period, bool isMonthPeriod)
        {
            if (isMonthPeriod)
            {
                return reportingPeriodService.GetAll()
                        .Where(r => r.MoneyBoxId == moneyBoxId &&
                            r.Type == ReportingPeriodType.Month &&
                            r.Period.Year == period.Year &&
                            r.Period.Month == period.Month)
                        .Select(s => s.Expence)
                        .SingleOrDefault();
            }
            else
            {
                return reportingPeriodService.GetAll()
                        .Where(r => r.MoneyBoxId == moneyBoxId &&
                            r.Type == ReportingPeriodType.Month &&
                            r.Period.Year == period.Year)
                        .Sum(s => s.Expence);
            }
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

        public static decimal GetBalanceForSpecPeriod(this IEntityBaseService<ReportingPeriod> reportingPeriodService, int moneyBoxId, DateTime period, bool isMonthPeriod)
        {
            if (isMonthPeriod)
            {
                return reportingPeriodService.GetAll()
                        .Where(r => r.MoneyBoxId == moneyBoxId &&
                            r.Type == ReportingPeriodType.Month &&
                            r.Period.Year == period.Year &&
                            r.Period.Month == period.Month)
                        .Select(s => s.Balance)
                        .SingleOrDefault();
            }
            else
            {
                return reportingPeriodService.GetAll()
                        .Where(r => r.MoneyBoxId == moneyBoxId &&
                            r.Type == ReportingPeriodType.Month &&
                            r.Period.Year == period.Year)
                        .Sum(s => s.Balance);
            }
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

        public static decimal GetBudgetForSpecPeriod(this IEntityBaseService<ReportingPeriod> reportingPeriodService, int moneyBoxId, DateTime period, bool isMonthPeriod)
        {
            if (isMonthPeriod)
            {
                return reportingPeriodService.GetAll()
                        .Where(r => r.MoneyBoxId == moneyBoxId &&
                            r.Type == ReportingPeriodType.Month &&
                            r.Period.Year == period.Year &&
                            r.Period.Month == period.Month)
                        .Select(s => s.Budget)
                        .SingleOrDefault();
            }
            else
            {
                return reportingPeriodService.GetAll()
                        .Where(r => r.MoneyBoxId == moneyBoxId &&
                            r.Type == ReportingPeriodType.Year &&
                            r.Period.Year == period.Year)
                        .Select(s => s.Budget)
                        .SingleOrDefault();
            }
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

        public static decimal GetBudgetBalanceForSpecPeriod(this IEntityBaseService<ReportingPeriod> reportingPeriodService, int moneyBoxId, DateTime period, bool isMonthPeriod)
        {
            if (isMonthPeriod)
            {
                return reportingPeriodService.GetAll()
                        .Where(r => r.MoneyBoxId == moneyBoxId &&
                            r.Type == ReportingPeriodType.Month &&
                            r.Period.Year == period.Year &&
                            r.Period.Month == period.Month)
                        .Select(s => s.BudgetBalance)
                        .SingleOrDefault();
            }
            else
            {
                return reportingPeriodService.GetBudgetForSpecPeriod(moneyBoxId, period, false) - reportingPeriodService.GetExpenceForSpecPeriod(moneyBoxId, period, false);
            }
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

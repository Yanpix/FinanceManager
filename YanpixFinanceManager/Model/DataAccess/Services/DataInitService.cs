using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YanpixFinanceManager.Model.Entities;
using YanpixFinanceManager.Model.Entities.Enums;

namespace YanpixFinanceManager.Model.DataAccess.Services
{
    public class DataInitService : IDataInitService
    {
        private IEntityBaseService<Category> _categoryService;

        private IEntityBaseService<Currency> _currencyService;

        private IEntityBaseService<CurrencyExchange> _currencyExchangeService;

        private IEntityBaseService<Account> _accountService;

        private IEntityBaseService<MoneyBox> _moneyBoxesService;

        private IEntityBaseService<ReportingPeriod> _reportingPeriodsService;

        public DataInitService(IEntityBaseService<Category> categoryService,
            IEntityBaseService<Currency> currencyService,
            IEntityBaseService<CurrencyExchange> currencyExchangeService,
            IEntityBaseService<Account> accountService,
            IEntityBaseService<MoneyBox> moneyBoxesService,
            IEntityBaseService<ReportingPeriod> reportingPeriodsService)
        {
            _categoryService = categoryService;
            _currencyService = currencyService;
            _currencyExchangeService = currencyExchangeService;
            _accountService = accountService;
            _moneyBoxesService = moneyBoxesService;
            _reportingPeriodsService = reportingPeriodsService;
        }

        public void Initialize()
        {
            List<Category> categories = new List<Category>()
            {
                new Category()
                {
                    Title = "Salary",
                    Type = TransactionType.Income,
                    Image = "/Assets/Icons/Light/appbar.currency.dollar.light.png"
                },
                new Category()
                {
                    Title = "Gifts",
                    Type = TransactionType.Income,
                    Image = "/Assets/Icons/Light/appbar.gift.png"
                },
                new Category()
                {
                    Title = "Nightlife",
                    Type = TransactionType.Expence,
                    Image = "/Assets/Icons/Light/appbar.drinks.beer.png"
                },
                new Category()
                {
                    Title = "Food",
                    Type = TransactionType.Expence,
                    Image = "/Assets/Icons/Light/appbar.food.silverware.png"
                },
                new Category()
                {
                    Title = "Electricity",
                    Type = TransactionType.Expence,
                    Image = "/Assets/Icons/Light/appbar.lightbulb.png"
                },
                new Category()
                {
                    Title = "Gifts",
                    Type = TransactionType.Expence,
                    Image = "/Assets/Icons/Light/appbar.gift.png"
                }
            };

            _categoryService.InsertAll(categories, false, false);

            Account account = new Account()
            {
                Username = "pavlo",
                Password = "qwerty",
                Email = "pavlo@gmail.com"
            };

            _accountService.Insert(account, false, false);

            List<Currency> currencies = new List<Currency>()
            {
                new Currency()
                {
                    Title = "United States Dollar",
                    Abbreviation = CurrencyType.USD,
                    Symbol = "$",
                    Image = "/Assets/Icons/Light/appbar.currency.dollar.light.png"
                },
                new Currency()
                {
                    Title = "European Euro",
                    Abbreviation = CurrencyType.EUR,
                    Symbol = "€",
                    Image = "/Assets/Icons/Light/appbar.currency.euro.png"
                },
                new Currency()
                {
                    Title = "Great Britain Pound",
                    Abbreviation = CurrencyType.GBP,
                    Symbol = "£",
                    Image = "/Assets/Icons/Light/appbar.currency.pound.png"
                },
                new Currency()
                {
                    Title = "Ukrainian Hryvna",
                    Abbreviation = CurrencyType.UAH,
                    Symbol = "₴",
                    Image = "/Assets/Icons/Light/appbar.currency.grivna.png"
                },
                new Currency()
                {
                    Title = "Russian Ruble",
                    Abbreviation = CurrencyType.RUB,
                    Symbol = "₽",
                    Image = "/Assets/Icons/Light/appbar.currency.rubles.png"
                }
            };

            _currencyService.InsertAll(currencies, false, false);

            List<CurrencyExchange> currencyExchanges = new List<CurrencyExchange>();

            foreach (Currency from in currencies)
            {
                foreach (Currency to in currencies.Where(c => c.Id != from.Id))
                {
                    currencyExchanges.Add(new CurrencyExchange()
                    {
                        From = from.Abbreviation.ToString(),
                        To = to.Abbreviation.ToString()
                    });
                }
            }

            _currencyExchangeService.InsertAll(currencyExchanges, false, false);

            MoneyBox euroMoneyBox = new MoneyBox()
            {
                Title = "Euro Money Box",
                YearBudget = 80000M,
                MonthBudget = 7000M
            };

            MoneyBox dollarMoneyBox = new MoneyBox()
            {
                Title = "Dollar Money Box",
                YearBudget = 90000M,
                MonthBudget = 7500M
            };

            _moneyBoxesService.Insert(euroMoneyBox, false, false);

            _moneyBoxesService.Insert(dollarMoneyBox, false, false);

            euroMoneyBox.PrimaryCurrency = _currencyService.Get(2);

            _moneyBoxesService.Update(euroMoneyBox, true);

            dollarMoneyBox.PrimaryCurrency = _currencyService.Get(1);

            _moneyBoxesService.Update(dollarMoneyBox, true);

            ReportingPeriod euroMoneyBox2016Period = new ReportingPeriod()
            {
                Type = ReportingPeriodType.Year,
                Period = DateTime.Now,
                Income = 12000M,
                Expence = 6000M,
                Budget = 80000M,
            };

            _reportingPeriodsService.Insert(euroMoneyBox2016Period, false, false);

            euroMoneyBox2016Period.MoneyBox = _moneyBoxesService.Get(1);

            euroMoneyBox2016Period.ParentPeriod = null;

            _reportingPeriodsService.Update(euroMoneyBox2016Period, true);

            ReportingPeriod euroMoneyBoxMarch2016Period = new ReportingPeriod()
            {
                Type = ReportingPeriodType.Month,
                Period = DateTime.Now,
                Income = 10000M,
                Expence = 5000M,
                Budget = 7000M,
            };

            _reportingPeriodsService.Insert(euroMoneyBoxMarch2016Period, false, false);

            euroMoneyBoxMarch2016Period.MoneyBox = _moneyBoxesService.Get(1);

            euroMoneyBoxMarch2016Period.ParentPeriod = _reportingPeriodsService.Get(1);

            _reportingPeriodsService.Update(euroMoneyBoxMarch2016Period, true);

            ReportingPeriod dollarMoneyBox2016Period = new ReportingPeriod()
            {
                Type = ReportingPeriodType.Year,
                Period = DateTime.Now,
                Income = 13000M,
                Expence = 10000M,
                Budget = 80000M,
            };

            _reportingPeriodsService.Insert(dollarMoneyBox2016Period, false, false);

            dollarMoneyBox2016Period.MoneyBox = _moneyBoxesService.Get(2);

            dollarMoneyBox2016Period.ParentPeriod = null;

            _reportingPeriodsService.Update(dollarMoneyBox2016Period, true);

            ReportingPeriod dollarMoneyBoxMarch2016Period = new ReportingPeriod()
            {
                Type = ReportingPeriodType.Month,
                Period = DateTime.Now,
                Income = 7000M,
                Expence = 8000M,
                Budget = 5000M,
            };

            _reportingPeriodsService.Insert(dollarMoneyBoxMarch2016Period, false, false);

            dollarMoneyBoxMarch2016Period.MoneyBox = _moneyBoxesService.Get(2);

            dollarMoneyBoxMarch2016Period.ParentPeriod = _reportingPeriodsService.Get(3);

            _reportingPeriodsService.Update(dollarMoneyBoxMarch2016Period, true);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using YanpixFinanceManager.Model.Entities;

namespace YanpixFinanceManager.Model.DataAccess.Services
{
    public class CurrencyRatesService : ICurrencyRatesService
    {
        private IEntityBaseService<CurrencyExchange> _currencyExchangeService;

        private List<KeyValuePair<string, decimal>> _dollarExchangeRates;

        public CurrencyRatesService(IEntityBaseService<CurrencyExchange> currencyExchangeService)
        {
            _currencyExchangeService = currencyExchangeService;
        }

        public decimal Exchange(string from, string to, decimal amount)
        {
            decimal coeff = _currencyExchangeService.GetAll()
                .Where(x => x.From.Equals(from) && x.To.Equals(to))
                .Select(s => s.Value)
                .SingleOrDefault();

            return amount * coeff;
        }

        public decimal GetExchangeCoeff(string from, string to)
        {
            return _currencyExchangeService.GetAll()
                .Where(x => x.From.Equals(from) && x.To.Equals(to))
                .Select(s => s.Value)
                .SingleOrDefault();
        }

        public async Task UpdateExchangeRates()
        {
            _dollarExchangeRates = await GetExchangeRatesFromWeb();

            if (_dollarExchangeRates.Any())
            {
                List<CurrencyExchange> currencyExchanges = _currencyExchangeService.GetAll().ToList();

                foreach (CurrencyExchange currencyExchange in currencyExchanges)
                {
                    decimal from = _dollarExchangeRates
                        .Where(x => x.Key.Equals(currencyExchange.From))
                        .Select(x => x.Value)
                        .SingleOrDefault();

                    decimal to = _dollarExchangeRates
                        .Where(x => x.Key.Equals(currencyExchange.To))
                        .Select(x => x.Value)
                        .SingleOrDefault();

                    currencyExchange.Value = 1 / from * to;
                    currencyExchange.UpdateDate = DateTime.Now;
                }

                _currencyExchangeService.InsertAll(currencyExchanges, true, false);
            }
        }

        private async Task<List<KeyValuePair<string, decimal>>> GetExchangeRatesFromWeb()
        {
            try
            {
                HttpClient http = new HttpClient();

                HttpResponseMessage response = await http.GetAsync(@"http://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.xchange where pair in (""USDEUR"", ""USDGBP"", ""USDRUB"", ""USDUAH"")&env=store://datatables.org/alltableswithkeys");

                string content = await response.Content.ReadAsStringAsync();

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.DtdProcessing = DtdProcessing.Ignore;

                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(content ?? ""));

                List<KeyValuePair<string, decimal>> dollarExchangeRates = new List<KeyValuePair<string, decimal>>();

                using (XmlReader xmlr = XmlReader.Create(stream, settings))
                {
                    var xmld = XDocument.Load(xmlr);

                    var xmlResults = xmld.Descendants("rate");

                    string key;
                    decimal value;

                    foreach (var item in xmlResults)
                    {
                        key = item.Element("Name").Value;
                        key = key.Substring(key.Length - 3);

                        value = decimal.Parse(item.Element("Rate").Value);

                        dollarExchangeRates.Add(new KeyValuePair<string, decimal>(key, value));
                    }

                    dollarExchangeRates.Add(new KeyValuePair<string, decimal>("USD", 1));
                }

                return dollarExchangeRates;
            }
            catch
            {
                return null;
            }
        }
    }
}

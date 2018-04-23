using NewWebTrader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewWebTrader.Controllers
{
    public class MarketDetailsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        List<double> avgList = new List<double>();
        List<Double> closingList = new List<double>();

        public MarketDetailsController()
        {
            _context = new ApplicationDbContext();
        }

        [Route("api/market/marketdetails/{type}/{startdate}/{range}")]
        [HttpGet]
        public IHttpActionResult GetSma(string type, string startdate, string range)
        {
            List<NewMarket> marketObject = new List<NewMarket>();
            List<SendDataViewModel> obj = new List<SendDataViewModel>();
            DateTime date1 = new DateTime(1899, 12, 30, 23, 59, 00);
            List<DateTime> xaxis = new List<DateTime>();
            DateTime stDate = DateTime.Parse(startdate);
            DateTime enDate = stDate.AddDays(10);
            marketObject = _context.NewMarkets.OrderBy(c => c.date).Where(c => c.type == type && c.time == date1 && c.date >= stDate && c.date <= enDate).ToList();

            for (int i = 0; i < marketObject.Count; i++)
            {
                closingList.Add(marketObject[i].closingPrice);
            }
            

            for (int j = 0; j <= closingList.Count - Convert.ToInt32(range); j++)
            {
                double sum = 0;
                double avg = 0;
                for (int i = j; i < j + Convert.ToInt32(range); i++)
                {
                    sum = sum + closingList[i];
                }
                avg = sum / Convert.ToInt32(range);
                avgList.Add(avg);
                SendDataViewModel sendData = new SendDataViewModel()
                {
                    date = DateTime.Parse(startdate).AddDays(j),
                    value = avg
                };
                obj.Add(sendData);
            }
            return Json(obj);
        }

        [Route("api/market/getema/{type}/{startdate}/{range}")]
        [HttpGet]
        public IHttpActionResult GetEma(string type, string startdate, string range)
        {
            this.GetSma(type, startdate, range);
            double initSMA = avgList.Last();
            double multiplire = 2 / (Convert.ToInt32(range) + 1);
            double EMA;
            List<SendDataViewModel> obj = new List<SendDataViewModel>();

            for (int i = 0; i < avgList.Count; i++)
            {
                EMA = (closingList.Last() - initSMA) * multiplire + initSMA;
                initSMA = initSMA + EMA;
                SendDataViewModel sendData = new SendDataViewModel()
                {
                    date = DateTime.Parse(startdate).AddDays(i),
                    value = EMA
                };
                obj.Add(sendData);
            }
            return Json(obj);
        }

        [Route("api/market/getrsi/{type}/{startdate}/{range}")]
        [HttpGet]
        public IHttpActionResult GetRsi(string type, string startdate, string range)
        {
            DateTime time = new DateTime(1899, 12, 30, 23, 59, 00);
            List<NewMarket> marketObject = new List<NewMarket>();
            List<NewMarket> newmarketObject = new List<NewMarket>();
            List<DateTime> xaxis = new List<DateTime>();
            List<SendDataViewModel> obj = new List<SendDataViewModel>();

            List<double> posDif = new List<double>();
            List<double> negDif = new List<double>();
            double avgLost = 0;
            double avgGain = 0;
            double totalLost = 0;
            double totalGain = 0;

            NewMarket newMarket = new NewMarket();
            DateTime dat = DateTime.Parse(startdate);
            DateTime enDate = dat.AddDays(10);
            int newRange = Convert.ToInt32(range);

            DateTime daybefore = dat.AddDays(-newRange - 1);
            marketObject = _context.NewMarkets.OrderBy(c => c.date).Where(c => c.type == type && c.date >= daybefore && c.date <= enDate && c.time == time).ToList();

            List<double> RSIList = new List<double>();

            for (int i = 0; i < newRange + 1; i++)
            {
                double priceDif = marketObject[i].closingPrice - marketObject[i + 1].closingPrice;
                if (priceDif < 0)
                {
                    totalLost = totalLost + priceDif;

                }
                else
                {
                    totalGain = totalGain + priceDif;
                }

            }
            avgLost = totalLost / newRange;
            avgGain = totalGain / newRange;
            var rsi = 100 - (100 / (1 + Math.Abs(avgGain / avgLost)));

            var rsiList = new List<double>();
            rsiList.Add(rsi);

            for (int i = newRange + 1; i < marketObject.Count - 1; i++)
            {
                var current = marketObject[i + 1].closingPrice - marketObject[i].closingPrice;
                if (current < 0)
                {
                    avgLost = ((avgLost * (newRange - 1)) + current) / newRange;
                }
                else
                {
                    avgGain = ((avgGain * (newRange - 1)) + current) / newRange;
                }
                rsi = 100 - (100 / (1 + Math.Abs(avgGain / avgLost)));

                SendDataViewModel sendData = new SendDataViewModel()
                {
                    date = dat.AddDays(i - newRange - 1),
                    value = rsi
                };
                obj.Add(sendData);
            }
            return Json(obj);
        }

    }
}

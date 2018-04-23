using NewWebTrader.Models;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace NewWebTrader.Controllers
{
    [Authorize]
    public class MarketController : Controller
    {
        private readonly ApplicationDbContext _context;
        List<double> avgList = new List<double>();
        List<Double> closingList = new List<double>();

        public MarketController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Market
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowMarket()
        {
            return View();
        }


        public ActionResult ShowCurrency()
        {
            return View();
        }

        public ActionResult ShowStock()
        {
            return View();
        }

        public ActionResult Indicator()
        {
            return View();
        }

        public ActionResult Google()
        {
            return View();
        }

        public ActionResult Microsoft()
        {
            return View();
        }

        public ActionResult EU()
        {
            return View();
        }


        public ActionResult Uj()
        {
            return View();
        }

        public ActionResult UC()
        {
            return View();
        }





        [HttpGet]
        public ActionResult EnterData()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EnterData(EnterData model)
        {
            if (ModelState.IsValid)
            {
                if (model.StartDate >= model.EndDate)
                {
                    ViewBag.Message = "Start date should be younger than the end date!";
                    return View();
                }
                List<NewMarket> marketObject = new List<NewMarket>();
                List<NewMarket> marketObject1 = new List<NewMarket>();
                List<DateTime> dateslist = new List<DateTime>();
                float[] averages = new float[4];
                List<DateTime> dateList = new List<DateTime>();
                DateTime date1 = new DateTime(1899, 12, 30, 23, 59, 00);
                List<DateTime> xaxis = new List<DateTime>();

                marketObject = _context.NewMarkets.OrderBy(c => c.date).Where(c => c.type == model.Market && c.time == date1 && c.date >= model.StartDate && c.date <= model.EndDate).ToList();

                for (int i = 0; i < marketObject.Count; i++)
                {
                    closingList.Add(marketObject[i].closingPrice);
                }

                for (int j = 0; j <= closingList.Count - model.Range; j++)
                {
                    double sum = 0;
                    double avg = 0;
                    for (int i = j; i < j + model.Range; i++)
                    {
                        sum = sum + closingList[i];
                    }
                    avg = sum / model.Range;
                    avgList.Add(avg);
                    xaxis.Add(model.StartDate.AddDays(j));
                }

                ViewBag.A = avgList;
                ViewBag.DateList = xaxis;
                return View();
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EnterDataEMA()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnterDataEMA(EnterData enterData)
        {
            if (ModelState.IsValid)
            {
                if (enterData.StartDate >= enterData.EndDate)
                {
                    ViewBag.Message = "Start date should be younger than the end date!";
                    return View();
                }
                this.EnterData(enterData);
                double initSMA = avgList.Last();
                double multiplire = 2 / (enterData.Range + 1);
                double EMA;
                List<double> EMAList = new List<double>();
                List<DateTime> xaxis = new List<DateTime>();

                for (int i = 0; i < avgList.Count; i++)
                {
                    EMA = (closingList.Last() - initSMA) * multiplire + initSMA;
                    initSMA = initSMA + EMA;
                    EMAList.Add(EMA);
                    xaxis.Add(enterData.StartDate.AddDays(i));
                }

                ViewBag.B = EMAList;
                ViewBag.DateList = xaxis;
                return View();
            }

            return View(enterData);
        }


        [HttpGet]
        public ActionResult EnterDataRSI()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnterDataRSI(EnterDataRSI enterDataRSI)
        {
            if (ModelState.IsValid)
            {
                if (enterDataRSI.StartDate >= enterDataRSI.EndDate)
                {
                    ViewBag.Message = "Start date should be younger than the end date!";
                    return View();
                }
                DateTime time = new DateTime(1899, 12, 30, 23, 59, 00);
                List<NewMarket> marketObject = new List<NewMarket>();
                List<NewMarket> newmarketObject = new List<NewMarket>();
                List<DateTime> xaxis = new List<DateTime>();

                List<double> posDif = new List<double>();
                List<double> negDif = new List<double>();
                double avgLost = 0;
                double avgGain = 0;
                double totalLost = 0;
                double totalGain = 0;

                NewMarket newMarket = new NewMarket();
                DateTime dat = enterDataRSI.StartDate;


                DateTime daybefore = dat.AddDays(-enterDataRSI.Range - 1);
                marketObject = _context.NewMarkets.OrderBy(c => c.date).Where(c => c.type == enterDataRSI.Market && c.date >= daybefore && c.date <= enterDataRSI.EndDate && c.time == time).ToList();

                List<double> RSIList = new List<double>();

                for (int i = 0; i < enterDataRSI.Range + 1; i++)
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
                avgLost = totalLost / enterDataRSI.Range;
                avgGain = totalGain / enterDataRSI.Range;
                var rsi = 100 - (100 / (1 + Math.Abs(avgGain / avgLost)));

                var rsiList = new List<double>();
                rsiList.Add(rsi);

                for (int i = enterDataRSI.Range + 1; i < marketObject.Count - 1; i++)
                {
                    var current = marketObject[i + 1].closingPrice - marketObject[i].closingPrice;
                    if (current < 0)
                    {
                        avgLost = ((avgLost * (enterDataRSI.Range - 1)) + current) / enterDataRSI.Range;
                    }
                    else
                    {
                        avgGain = ((avgGain * (enterDataRSI.Range - 1)) + current) / enterDataRSI.Range;
                    }
                    rsi = 100 - (100 / (1 + Math.Abs(avgGain / avgLost)));
                    rsiList.Add(rsi);

                    xaxis.Add(enterDataRSI.StartDate.AddDays(i - enterDataRSI.Range - 1));
                }
                ViewBag.RsiList = rsiList;
                ViewBag.DateList = xaxis;
                return View();
            }

            return View(enterDataRSI);
        }



        [HttpGet]
        public ActionResult EnterDataROC()
        {
            return View();
        }


        [HttpPost]
        public ActionResult EnterDataROC(EnterDataROC enterDataROC)
        {
            if (ModelState.IsValid)
            {
                Double ROC;
                List<NewMarket> marketObject = new List<NewMarket>();
                DateTime time = new DateTime(1899, 12, 30, 23, 59, 00);
                int p = enterDataROC.period;
                NewMarket newMarket = new NewMarket();
                DateTime currentdate = enterDataROC.SelectDate;
                DateTime daybefore = currentdate.AddDays(-p);

                marketObject = _context.NewMarkets.OrderBy(c => c.date).Where(c => c.type == enterDataROC.Market && c.date >= daybefore && c.date <= enterDataROC.SelectDate && c.time == time).ToList();

                ROC = (marketObject[marketObject.Count - 1].closingPrice - marketObject[0].closingPrice) * 100 / marketObject[0].closingPrice;

                ViewBag.C = ROC;
                return View();
            }

            return View(enterDataROC);
        }




        [HttpGet]
        public ActionResult EnterDataSD()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnterDataSD(EnterDataSD enterDataSD)
        {
            if (ModelState.IsValid)
            {
                if (enterDataSD.StartDate >= enterDataSD.EndDate)
                {
                    ViewBag.Message = "Start date should be younger than the end date!";
                    return View();
                }
                int i;
                double SD;
                double add;
                double addlist = 0;

                List<NewMarket> marketObject = new List<NewMarket>();
                DateTime time = new DateTime(1899, 12, 30, 23, 59, 00);
                NewMarket newMarket = new NewMarket();

                DateTime dat1 = enterDataSD.StartDate;
                DateTime dat2 = enterDataSD.EndDate;

                marketObject = _context.NewMarkets.OrderBy(c => c.date).Where(c => c.type == enterDataSD.Market && c.date >= enterDataSD.StartDate && c.time == time && c.date <= enterDataSD.EndDate && c.time == time).ToList();

                for (i = 0; i < marketObject.Count - 1; i++)
                {
                    add = marketObject[i].closingPrice * marketObject[i].closingPrice;
                    addlist += add;
                }

                SD = Math.Sqrt(addlist) / marketObject.Count;
                ViewBag.B = SD;
                return View();
            }

            return View(enterDataSD);
        }

        public ActionResult EnterDataStoch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnterDataStoch(EnterDataROC model)
        {
            if (ModelState.IsValid)
            {
                List<NewMarket> marketObject = new List<NewMarket>();
                
                DateTime time = new DateTime(1899, 12, 30, 23, 59, 00);
                DateTime date = model.SelectDate;
                DateTime before = date.AddDays(-model.period);


                marketObject = _context.NewMarkets.OrderBy(c => c.date).Where(c => c.type == model.Market && c.date >= before && c.date <= model.SelectDate && c.time == time).ToList();
                var minimumLow = marketObject.Select(c => c.lowestPrice).Min();
                var maximumHigh = marketObject.Select(c => c.highestPrice).Max();
                var currClose = marketObject.OrderByDescending(c => c.time).Select(c => c.closingPrice).FirstOrDefault();

                var STOCH = ((currClose - minimumLow) / (maximumHigh - minimumLow)) * 100;

                ViewBag.A = STOCH;
                return View();
            }

            return View(model);
        }


    }


}
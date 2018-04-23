using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using System;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NewWebTrader.Models;
using Newtonsoft.Json;

namespace NewWebTrader.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        public readonly string _baseUrl = "https://newsapi.org";

        public async Task<ActionResult> Index()
        {
            NewsHeadLinesViewModel newsHeadLines = new NewsHeadLinesViewModel();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await client.GetAsync("v2/everything?q=forex&sortBy=publishedAt&apiKey=0f9800346e62486f95977914eef6a2d2");
                
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseResult = await responseMessage.Content.ReadAsStringAsync();
                    newsHeadLines = JsonConvert.DeserializeObject<NewsHeadLinesViewModel>(responseResult);
                }
            }
            ViewBag.News = newsHeadLines;
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VerseOfTheDay.Models;

namespace VerseOfTheDay.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly HttpClient client = new HttpClient();
        private VerseRepository _repo;
        private readonly string _apiKey;
        private readonly string _apiUri;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _apiKey = config["klove-api-key"];
            _apiUri = config["klove-api-uri"];
            _logger = logger;
            _repo = new VerseRepository();
        }

        public IActionResult Index()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);

            var stringTask = client.GetStringAsync(_apiUri + "getversesbydate?siteId=1&startdate=5/4/2021&PageSize=10");
            stringTask.Wait();
            var verseJson = stringTask.Result;

            var verses = JsonSerializer.Deserialize<VersesModel>(verseJson);

            Console.WriteLine(verseJson);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}
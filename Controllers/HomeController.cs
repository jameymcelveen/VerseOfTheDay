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
        private DataContext _context;
        private readonly string _apiKey;
        private readonly string _apiUri;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, DataContext context)
        {
            _apiKey = config["klove-api-key"];
            _apiUri = config["klove-api-uri"];
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string startDate, string pageSize)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);

            startDate = !string.IsNullOrEmpty(startDate) ? DateTime.Parse(startDate).ToShortDateString() : DateTime.Now.ToShortDateString();
            pageSize ??= "1";
            var uri = string.Format("{0}getversesbydate?siteId=1&startdate={1}&PageSize={2}", _apiUri, startDate, pageSize);
            var stringTask = client.GetStringAsync(uri);
            stringTask.Wait();
            var verseJson = stringTask.Result;

            var versesModel = JsonSerializer.Deserialize<VersesModel>(verseJson);

            Console.WriteLine(verseJson);

            return View(versesModel);
        }

        public IActionResult Favorites()
        {
            var versesModel = new VersesModel() { Verses = _context.Verses.ToList() };
            return View(versesModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult Mark(string date, string how)
        {
            if (how == "on")
            {
                AddFavorite(date);
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        private void AddFavorite(string date)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);

            var stringDate = DateTime.Parse(date).ToShortDateString();
            var stringTask = client.GetStringAsync(_apiUri + "getversesbydate?siteId=1&startdate="+ stringDate + "&PageSize=1");
            stringTask.Wait();
            var verseJson = stringTask.Result;
            var versesModel = JsonSerializer.Deserialize<VersesModel>(verseJson);
            var apiVerse = versesModel.Verses.FirstOrDefault();
            var verse = _context.Verses
                .FirstOrDefault(s => s.Id == apiVerse.Id);
            if (verse != null)
            {
                verse.Favorite = true;
                _context.SaveChanges();
            }
            else
            {
                _context.Verses.Add(apiVerse);
                _context.SaveChanges();
            }
        }

        private void RemoveFavorite(string id)
        {
            
        }
    }
}
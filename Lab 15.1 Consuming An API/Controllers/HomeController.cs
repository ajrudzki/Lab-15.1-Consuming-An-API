using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lab_15._1_Consuming_An_API.Models;
using System.Net.Http;

namespace Lab_15._1_Consuming_An_API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        /*public async Task<IActionResult> GenerateID()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://deckofcardsapi.com");
            var response = await client.GetAsync("/api/deck/new/shuffle/?deck_count=1");
            Cards info = await response.Content.ReadAsAsync<Cards>();
            return View(info);
        }*/       

        public async Task<IActionResult> DrawCards(string drawAgain, string deck_id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://deckofcardsapi.com");
            var response = await client.GetAsync("/api/deck/new/shuffle");
            Hand hand;
            if (string.IsNullOrEmpty(drawAgain))
            {
                Cards cards = await response.Content.ReadAsAsync<Cards>();
                response = await client.GetAsync($"/api/deck/{cards.deck_id}/draw/?count=5");
                hand = await response.Content.ReadAsAsync<Hand>();
            }
            else
            {
                response = await client.GetAsync($"/api/deck/{deck_id}/draw/?count=5");
                hand = await response.Content.ReadAsAsync<Hand>();
            }
            return View(hand);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

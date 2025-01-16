using Microsoft.AspNetCore.Mvc;
using Order_In_Stores.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace Order_In_Stores.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, HttpClient httpClient)
        {
            _logger = logger;
            _context = context;
            _httpClient = httpClient;
        }

        // 4a. Strona "Dane pobrane z webserwisu"
        public async Task<IActionResult> WebApiData()
        {
            // Wysyłamy zapytanie do Web API
            var response = await _httpClient.GetAsync("https://localhost:7059/api/orders");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var orders = JsonSerializer.Deserialize<List<Order_In_Stores.Models.OrderApiDto>>(data);
                return View(orders);
            }

            return View("Error");
        }

        // 4b. Strona "Dane pobrane przy pomocy Entity Framework"
        public async Task<IActionResult> EntityFrameworkData()
        {
            var orders = await _context.Orders
                .Include(o => o.CustomerAddress)  // Wczytanie adresu klienta
                .Include(o => o.OrderLines)       // Wczytanie linii zamówienia
                .Where(o => o.OrderLines.Sum(ol => ol.GrossPrice) >= 150) // Filtracja zamówień powyżej 150zł
                .ToListAsync();

            var report = orders
                .GroupBy(o => o.PaymentType)
                .Select(g => new PaymentSummary
                {
                    PaymentType = g.Key,
                    TotalOrders = g.Count(),
                    TotalGrossValue = g.Sum(o => o.OrderLines.Sum(ol => ol.GrossPrice))
                }).ToList();

            return View(report);
        }

        // Strona główna - tylko dla przykładu
        public IActionResult Index()
        {
            return View();
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

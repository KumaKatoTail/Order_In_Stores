using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order_In_Stores.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Order_In_Stores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            
            var orders = await _context.Orders
                .Include(o => o.CustomerAddress) 
                .Where(o => o.StoreNumber % 2 == 0 && o.CustomerAddress.City.Contains("w")) // Filtrowanie zamówień (parzyste sklepy, miasto zawiera 'w')
                .Select(o => new OrderApiDto
                {
                    Id = o.Id,
                    StoreName = o.StoreName,
                    City = o.CustomerAddress.City,
                    NetValue = o.OrderLines.Sum(ol => ol.NetPrice) // Suma wartości netto
                })
                .ToListAsync();

            return Ok(orders);
        }
    }

   
}

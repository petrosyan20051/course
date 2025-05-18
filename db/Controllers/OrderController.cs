using db.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace db.Controllers {

    [ApiController]
    [Route("Ñontrollers")]
    public class OrdersController : ControllerBase {
        private readonly ApplicationContext _context;

        public OrdersController(ApplicationContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders() {
            return await _context.Orders.ToListAsync();
        }
    }
}
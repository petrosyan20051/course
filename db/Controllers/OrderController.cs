using db.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace db.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase {
        private readonly ApplicationContext _context;

        public OrdersController(ApplicationContext context) {
            _context = context;
        }

        // Get: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders() {
            return await _context.Orders.ToListAsync();
        }

        //Get: api/orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id) {
            var order = await _context.Orders.FindAsync(id);
            return order == null ? NotFound() : order;
        }
    }
}
using db.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace db.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase {
        private readonly OrderContext _context;

        public OrdersController(OrderContext context) {
            _context = context;
        }

        // Get: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders() {
            return await _context.Orders.ToListAsync();
        }

        //Get: api/orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id) {
            var order = await _context.Orders.FindAsync(id);
            return order is null ? NotFound() : order;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Order updated) {
            if (id != updated.Id) return BadRequest();
            _context.Entry(updated).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var book = await _context.Orders.FindAsync(id);
            if (book is null) return NotFound();
            _context.Orders.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
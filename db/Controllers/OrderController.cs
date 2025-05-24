using db.Contexts;
using db.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using IdType = int;

namespace db.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : BaseCrudController<Order, IdType> {
        public OrderController(IRepository<Order, int> repository) : base(repository) { }
        
        protected override int GetEntityId(Order entity) {
            return entity.Id;
        }
    }
}
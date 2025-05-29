using db.Models;
using db.Repositories;
using Microsoft.AspNetCore.Mvc;

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
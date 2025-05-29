using db.Models;
using db.Repositories;
using Microsoft.AspNetCore.Mvc;

using IdType = int;

namespace db.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : BaseCrudController<Customer, IdType> {
        public CustomerController(IRepository<Customer, int> repository) : base(repository) { }

        protected override int GetEntityId(Customer entity) {
            return entity.Id;
        }
    }
}
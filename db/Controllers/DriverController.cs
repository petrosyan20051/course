using db.Models;
using db.Repositories;
using Microsoft.AspNetCore.Mvc;

using TypeId = int;

namespace db.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : BaseCrudController<Driver, TypeId> {
        public DriverController(IRepository<Driver, int> repository) : base(repository) { }

        protected override int GetEntityId(Driver entity) {
            return entity.Id;
        }
    }
}

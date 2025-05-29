using db.Models;
using db.Repositories;
using Microsoft.AspNetCore.Mvc;

using TypeId = int;

namespace db.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TransportVehicleController : BaseCrudController<TransportVehicle, TypeId> {
        public TransportVehicleController(IRepository<TransportVehicle, int> repository) : base(repository) { }

        protected override int GetEntityId(TransportVehicle entity) {
            return entity.Id;
        }
    }
}

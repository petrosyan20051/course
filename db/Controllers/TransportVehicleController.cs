using db.Models;
using db.Repositories;
using Microsoft.AspNetCore.Mvc;

using TypeId = int;

namespace db.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TransportVehicleController : BaseCrudController<TransportVehicle, TypeId> {
        public TransportVehicleController(IRepository<TransportVehicle, TypeId> repository) : base(repository) { }

        protected override int GetEntityId(TransportVehicle entity) {
            return entity.Id;
        }

        [HttpPost("RecoverById")]
        public virtual async Task<IActionResult> RecoverAsync(TypeId id) {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) {
                entity.isDeleted = null;
                entity.WhenChanged = DateTime.Now;
                await _repository.UpdateAsync(entity);
                return Ok(new string("Восстановление прошло успешно"));
            }

            return NotFound(new string("Сущность не удалена или не найдена"));
        }
    }
}

using db.Models;
using db.Repositories;
using Microsoft.AspNetCore.Mvc;

using TypeId = int;

namespace db.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : BaseCrudController<Order, TypeId> {
        public OrderController(IRepository<Order, TypeId> repository) : base(repository) { }

        protected override TypeId GetEntityId(Order entity) {
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
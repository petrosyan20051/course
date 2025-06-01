using db.Models;
using db.Repositories;
using Microsoft.AspNetCore.Mvc;

using TypeId = int;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseCrudController<TEntity, TKey> : ControllerBase
    where TEntity : BaseModel {
    protected readonly IRepository<TEntity, TKey> _repository;

    public BaseCrudController(IRepository<TEntity, TKey> repository) {
        _repository = repository;
    }

    // GET: api/{entity}
    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAll() {
        return Ok(await _repository.GetAllAsync());
    }

    // GET: api/{entity}
    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TEntity>> Get(TKey id) {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    // POST: api/{entity}
    [HttpPost]
    public virtual async Task<ActionResult<TEntity>> Create([FromBody] TEntity entity) {
        await _repository.AddAsync(entity);
        return CreatedAtAction(nameof(Get), new { id = GetEntityId(entity) }, entity);
    }

    // PUT: api/{entity}
    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update(TKey id, [FromBody] TEntity entity) {
        if (!id.Equals(GetEntityId(entity))) {
            return BadRequest();
        }

        await _repository.UpdateAsync(entity);
        return NoContent();
    }

    // DELETE: api/{entity}
    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(TKey id) {
        await _repository.DeleteAsync(id);
        return NoContent();
    }

    // GET: api/{NewIdToAdd}
    [HttpGet("NewIdToAdd")]
    public virtual async Task<TypeId> NewIdToAdd() {
        var entities = await _repository.GetAllAsync();
        if (entities == null)
            return -1; // entities are not found

        // Get All deleted Ids in ascending order
        var deletedIds = entities
            .Where(e => e.isDeleted != null)
            .Select(e => e.Id)
            .OrderBy(id => id)
            .ToList();
        if (deletedIds.Any())
            return deletedIds.First(); // return first free id
        var usedIds = new HashSet<TypeId>(entities.Select(c => c.Id).Where(id => id > 0).OrderBy(id => id));
        for (TypeId i = 1; i < TypeId.MaxValue; i++) {
            if (!usedIds.Contains(i))
                return i;
        }
        return TypeId.MaxValue; // maybe all seats are reserved
    }

    // Update: api/RecoverById
    [HttpGet("RecoverById")]
    public virtual async Task<IActionResult> RecoverAsync(TKey id) {
        var entity = await _repository.GetByIdAsync(id);
        if (entity != null) {
            entity.isDeleted = null;
            entity.WhenChanged = DateTime.Now;

            return Ok("Восстановление прошло успешно");
        }

        return NotFound("Сущность не найдено или уже существует");
    }

    protected abstract TKey GetEntityId(TEntity entity);
}
using db.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseCrudController<TEntity, TKey> : ControllerBase
    where TEntity : class {
    protected readonly IRepository<TEntity, TKey> _repository;

    public BaseCrudController(IRepository<TEntity, TKey> repository) {
        _repository = repository;
    }

    // GET: api/{entity}
    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAll() {
        return Ok(await _repository.GetAllAsync());
    }

    // GET: api/{entity}/5
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

    // PUT: api/{entity}/5
    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update(TKey id, [FromBody] TEntity entity) {
        if (!id.Equals(GetEntityId(entity))) {
            return BadRequest();
        }

        await _repository.UpdateAsync(entity);
        return NoContent();
    }

    // DELETE: api/{entity}/5
    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(TKey id) {
        await _repository.DeleteAsync(id);
        return NoContent();
    }

    protected abstract TKey GetEntityId(TEntity entity);
}
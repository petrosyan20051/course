using db.Classes;
using db.Interfaces;
using db.Models;
using db.Repositories;
using DbAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using static db.Interfaces.IInformation;

using TypeId = int;

namespace db.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class CredentialController : BaseCrudController<Credential, TypeId> {
        private readonly RoleRepository _roleRepository;
        private readonly IJwtService _jwtService;

        public CredentialController(CredentialRepository credentialRepository, RoleRepository roleRepository,
            IJwtService jwtService) : base(credentialRepository) {
            _roleRepository = roleRepository;
            _jwtService = jwtService;
        }

        protected int GetEntityId(Credential entity) {
            return entity.Id;
        }

        [HttpPost("Login")]
        [EnableRateLimiting("LoginPolicy")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginPrompt request) {
            // Standart checks
            CredentialRepository _credentialRepository = (CredentialRepository)_repository;
            if (_credentialRepository == null || _roleRepository == null || _jwtService == null)
                return StatusCode(500, new { message = "Внутренняя ошибка" });

            try {
                // Whether credential with such username exists
                var credential = await _credentialRepository.GetByUserNameAsync(request.Login);
                if (credential == null || credential.IsDeleted != null)
                    return Unauthorized(new { message = $"Пользователем с именем \"{request.Login}\" не существует" });

                // Password verification
                if (!PasswordHasher.VerifyPassword(request.Password, credential.Password))
                    return Unauthorized(new { messsage = "Введен неверный пароль" });

                // Whether such role exists 
                var role = await _roleRepository.GetByIdAsync(credential.RoleId);
                if (role == null || role.IsDeleted != null)
                    return BadRequest(new { message = "Внутренняя ошибка" });

                var token = _jwtService.GenerateToken(credential, role);

                var response = new LoginResponse {
                    UserId = credential.Id,
                    Username = credential.Username,
                    Token = token,
                    TokenExpireTime = DateTime.UtcNow.AddMinutes(_jwtService.GetTokenLifeTime()),
                    CanGet = role.CanGet,
                    CanPost = role.CanPost,
                    CanUpdate = role.CanUpdate,
                    CanDelete = role.CanDelete,
                };

                return Ok(response);
            } catch (Exception ex) {
                return StatusCode(500, new { message = $"Внутренняя ошибка: {ex.Message}" });
            }
        }

        // TODO: make registration
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterPrompt request) {
            // Standart checks
            CredentialRepository _credentialRepository = (CredentialRepository)_repository;
            if (_credentialRepository == null || _roleRepository == null)
                return StatusCode(500, new { message = "Внутренняя ошибка" });

            // Check whether person who registrates current one exists
            if (await _credentialRepository.GetByUserNameAsync(request.WhoRegister) == null)
                return BadRequest(new {
                    message = $"Пользователем с именем \"{request.WhoRegister}\", " +
                    $"который регистрирует нового пользователя не существует"
                });

            // Whether user with such name exists
            var credential = await _credentialRepository.GetByUserNameAsync(request.UserName);
            if (credential != null)
                return BadRequest(new { message = $"Пользователь с именем \"{request.UserName}\" уже существует" });

            // Check whether password is strong
            if (!PasswordHasher.IsPasswordStrong(request.Password))
                return BadRequest(new { message = "Введенный пароль ненадёжен" });

            // Whether selected role exists
            var role = await _roleRepository.GetByUserRights(request.RegisterRights);
            if (role == null)
                return BadRequest(new { message = "Введенные права пользователя не существуют" });

            var response = new RegisterResponse {
                UserName = request.UserName,
                CanGet = true,
                CanPost = request.RegisterRights != UserRights.Basic,
                CanUpdate = request.RegisterRights != UserRights.Basic,
                CanDelete = request.RegisterRights == UserRights.Admin
            };

            await _credentialRepository.AddAsync(new Credential {
                RoleId = role.Id,
                Username = request.UserName,
                Password = PasswordHasher.HashPassword(request.Password),
                WhoAdded = request.WhoRegister, 
                WhenAdded = DateTime.Now,
            });

            return Ok(new {
                message = "Пользователь успешно зарегестрирован",
                userName = request.UserName,
                response
            });
        }


        // GET: api/{entity}/GetAll
        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<IEnumerable<Credential>>> GetAll() {
            return Ok(await _repository.GetAllAsync());
        }

        // GET: api/{entity}/GetById
        [HttpGet("GetById")]
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<Credential>> Get(TypeId id) {
            var entity = await _repository.GetByIdAsync(id);
            return entity is null ? NotFound() : Ok(entity);
        }

        // POST: api/{entity}/Post
        [HttpPost("Post")]
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<Credential>> Create([FromBody] Credential entity) {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = GetEntityId(entity) }, entity);
        }

        // PUT: api/{entity}/UpdateById
        [HttpPut("UpdateById")]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Update(TypeId id, [FromBody] Credential entity) {
            if (!id.Equals(GetEntityId(entity))) {
                return BadRequest();
            }

            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        // DELETE: api/{entity}/DeleteById
        [HttpDelete("DeleteById")]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Delete(TypeId id) {
            await _repository.SoftDeleteAsync(id);
            return NoContent();
        }

        // Update: api/{entity}/RecoverById
        [HttpGet("RecoverById")]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> RecoverAsync(TypeId id) {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) {
                entity.IsDeleted = null;
                entity.WhenChanged = DateTime.Now;

                return Ok("Восстановление прошло успешно");
            }

            return NotFound("Сущность не найдено или уже существует");
        }
    }
}
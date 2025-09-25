﻿using db.Classes;
using db.Interfaces;
using db.Models;
using db.Repositories;
using DbAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using static db.Interfaces.IInformation;

using TypeId = int;

namespace db.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class CredentialController : BaseCrudController<Credential, TypeId> {
        private readonly RoleRepository _roleRepository;
        private readonly IJwtService _jwtService;
        private readonly ILogger<CredentialController> _logger;

        public CredentialController(CredentialRepository credentialRepository, RoleRepository roleRepository,
            IJwtService jwtService, ILogger<CredentialController> logger) : base(credentialRepository) {
            _roleRepository = roleRepository;
            _jwtService = jwtService;
            _logger = logger;
        }

        protected int GetEntityId(Credential entity) {
            return entity.Id;
        }

        [HttpPost("login")]
        [EnableRateLimiting("LoginPolicy")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginPrompt request) {
            _logger.LogInformation($"Начало попытки авторизации для пользователя \"{request.Login}\"");

            // Standart checks
            CredentialRepository _credentialRepository = (CredentialRepository)_repository;
            if (_credentialRepository == null || _roleRepository == null || _jwtService == null) {
                _logger.LogCritical("Внутренняя ошибка: репозитории и сервисы не проинициализированы");
                return StatusCode(500, new { message = "Внутренняя ошибка" });
            }

            try {
                // Whether credential with such username exists
                var credential = await _credentialRepository.GetByUserNameAsync(request.Login);
                if (credential == null || credential.IsDeleted != null) {
                    _logger.LogError($"Пользователем с именем \"{request.Login}\" не существует");
                    return Unauthorized(new { message = $"Пользователем с именем \"{request.Login}\" не существует" });
                }
                    

                // Password verification
                if (!PasswordHasher.VerifyPassword(request.Password, credential.Password)) {
                    _logger.LogError($"Введен неверный логин или пароль пользователя \"{request.Login}\"");
                    return Unauthorized(new { messsage = "Введен неверный логин или пароль" });
                }
                    

                // Whether such role exists 
                var role = await _roleRepository.GetByIdAsync(credential.RoleId);
                if (role == null || role.IsDeleted != null) {
                    _logger.LogCritical("Внутренняя ошибка: введенная роль пользователя не существует в БД");
                    return BadRequest(new { message = "Внутренняя ошибка" });
                }

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

                _logger.LogInformation($"Авторизация пользователя \"{request.Login}\" прошла успешно");
                return Ok(response);
            } catch (Exception ex) {
                _logger.LogCritical($"Внутренняя ошибка: {ex.Message}");
                return StatusCode(500, new { message = $"Внутренняя ошибка: {ex.Message}" });
            }
        }

        // TODO: make registration
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterPrompt request) {
            string logRights = request.RegisterRights switch {
                UserRights.Basic => "простого пользователя",
                UserRights.Editor => "редактора",
                UserRights.Admin => "администратора"
            };

            _logger.LogInformation($"Начало попытки регистрации пользователя \"{request.UserName}\" " +
                $"с правами {logRights}. " +
                $"Админ-регистратор \"{request.WhoRegister ?? "отсутствует"}\"");

            // Standart checks
            CredentialRepository _credentialRepository = (CredentialRepository)_repository;
            if (_credentialRepository == null || _roleRepository == null) {
                _logger.LogCritical("Внутренняя ошибка: репозитории и сервисы не проинициализированы");
                return StatusCode(500, new { message = "Внутренняя ошибка" });
            }


            // Check whether person who registrates current one exists
            if (request.RegisterRights != UserRights.Basic && (request.WhoRegister.IsNullOrEmpty() || 
                await _credentialRepository.GetByUserNameAsync(request.WhoRegister) == null)) {
                _logger.LogError($"Администратор с именем \"{request.WhoRegister}\", " +
                    $"который регистрирует нового пользователя не существует");
                return BadRequest(new {
                    message = $"Администратор с именем \"{request.WhoRegister}\", " +
                    $"который регистрирует нового пользователя не существует"
                }); 
            }

            // Whether user with such name exists
            var credential = await _credentialRepository.GetByUserNameAsync(request.UserName);
            if (credential != null) {
                _logger.LogError($"Пользователь с именем \"{request.UserName}\" уже существует");
                return BadRequest(new { message = $"Пользователь с именем \"{request.UserName}\" уже существует" });
            }
                
            // Check whether password is strong
            if (!PasswordHasher.IsPasswordStrong(request.Password)) {
                _logger.LogError("Введенный пароль ненадёжен");
                return BadRequest(new { message = "Введенный пароль ненадёжен" });
            }
                
            // Whether selected role exists
            var role = await _roleRepository.GetByUserRights(request.RegisterRights);
            if (role == null) {
                _logger.LogError("\"Введенные права пользователя не существуют\"");
                return BadRequest(new { message = "Введенные права пользователя не существуют" }); 
            }
                

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
                WhoAdded = request.WhoRegister.IsNullOrEmpty() ? request.UserName : request.WhoRegister,
                WhenAdded = DateTime.Now,
            });

            _logger.LogInformation($"Регистрация пользователя \"{request.UserName}\" прошла успешно");

            return Ok(response);
        }


        // GET: api/{entity}/
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<IEnumerable<Credential>>> GetAll() {
            _logger.LogWarning($"Администратор {User.Identity.Name} сделал запрос ко всем учетным записям");
            return Ok(await _repository.GetAllAsync());
        }

        // GET: api/{entity}/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<Credential>> Get(TypeId id) {
            var entity = await _repository.GetByIdAsync(id);
            _logger.LogWarning($"Администратор {User.Identity.Name} сделал запрос учетной записи с ID = {id}");
            return entity is null ? NotFound(new { message = $"Сущность с ID = {id} не найдена" }) : Ok(entity);
        }

        // POST: api/{entity}/
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<Credential>> Create([FromBody] Credential entity) {
            try {
                await _repository.AddAsync(entity);
                _logger.LogInformation($"Администратор {User.Identity.Name} создал новую учетную запись с ID = {entity.Id}");
            } catch (Exception ex) {
                _logger.LogError($"Администратору {User.Identity.Name} не удалось создать новую учетную запись с ID = {entity.Id}. " +
                    $"Причина: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }

            return CreatedAtAction(nameof(Get), new { id = GetEntityId(entity) }, entity);
        }

        // PUT: api/{entity}/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Update(TypeId id, [FromBody] Credential entity) {
            _logger.LogWarning($"Администратор {User.Identity.Name} пытается обновить данные учетной записи с ID = {entity.Id}");
            if (!id.Equals(GetEntityId(entity))) {
                _logger.LogError($"Администратору {User.Identity.Name} не удалось обновить данные учетной записи с ID = {entity.Id}. " +
                    $"Причина: сущность не найдена");
                return BadRequest(new { message = $"Сущность с ID = {id} не найдена" });
            }

            _logger.LogInformation($"Администратор {User.Identity.Name} обновил учетную запись с ID = {entity.Id}");
            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        // DELETE: api/{entity}/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Delete(TypeId id) {
            _logger.LogWarning($"Администратор {User.Identity.Name} пытается удалить учетную запись с ID = {id}");
            if (await _repository.GetByIdAsync(id) == null) {
                _logger.LogError($"Администратору {User.Identity.Name} не удалось удалить учетную запись с ID = {id}. " +
                    $"Причина: сущность не найдена");
                return BadRequest(new { message = $"Сущность с ID = {id} не найдена" });
            }

            _logger.LogInformation($"Администратор {User.Identity.Name} удалил учетную запись с ID = {id}");
            await _repository.SoftDeleteAsync(id);
            return NoContent();
        }

        // Update: api/{entity}/{id}/recover
        [HttpPatch("{id}/recover")]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> RecoverAsync(TypeId id) {
            _logger.LogWarning($"Администратор {User.Identity.Name} пытается восстановить учетную запись с ID = {id}");

            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) {
                entity.IsDeleted = null;
                entity.WhenChanged = DateTime.Now;
                await _repository.UpdateAsync(entity);

                _logger.LogInformation($"Администратор {User.Identity.Name} восстановил учетную запись с ID = {id}");
                return Ok("Восстановление прошло успешно");
            }

            _logger.LogError($"Администратору {User.Identity.Name} не удалось восстановить учетную запись с ID = {id}. " +
                    $"Причина: сущность не найдена");
            return NotFound(new { message = $"Сущность с ID = {id} не найдена или уже существует" });
        }
    }
}
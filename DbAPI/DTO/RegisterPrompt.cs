﻿using System.ComponentModel.DataAnnotations;
using static db.Interfaces.IInformation;

namespace DbAPI.DTO {
    public class RegisterPrompt {
        [Required(ErrorMessage = "Имя пользователя обязательно")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Пароль пользователя обязателен")]
        public required string Password { get; set; }

        public string? WhoRegister { get; set; } = string.Empty;

        [Required(ErrorMessage = "Тип регистрируемого пользователя обязателен")]
        public UserRights RegisterRights { get; set; }
    }
}

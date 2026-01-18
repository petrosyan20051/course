import { AuthService } from "./authorize.js";

/* Обработчики для компонентов */

window.showAuthorizeForm = showAuthorizeForm;
window.showRegisterForm = showRegisterForm;
window.showForgotPasswordForm = showForgotPasswordForm;
window.AuthService = AuthService;
window.registerShowGeneratePassword = registerShowGeneratePassword;
window.generateSecurePassword = generateSecurePassword;
window.insertGeneratedPassword = insertGeneratedPassword;

function showAuthorizeForm() {
    // Скрываем все формы
    document.querySelectorAll('.form').forEach(form => {
        form.classList.remove('active');
    });
    // Показываем авторизацию
    document.querySelector('.authorize-form').classList.add('active');
}

function showRegisterForm() {
    document.querySelectorAll('.form').forEach(form => {
        form.classList.remove('active');
    });
    document.querySelector('.register-form').classList.add('active');
}

function showForgotPasswordForm() {
    document.querySelectorAll('.form').forEach(form => {
        form.classList.remove('active');
    });
    document.querySelector('.recovery-form').classList.add('active');
}

function registerShowGeneratePassword() {
    // Проверяем, не существует ли уже подсказки
    let tipContainer = document.getElementById('generate-password-form');
    
    if (!tipContainer) {
        tipContainer = document.createElement('div');
        tipContainer.id = 'generate-password-form';
        tipContainer.classList.add('generate-password-form', 'active');
        
        const passwordInput = document.getElementById('passwordRegister');
        const inputRect = passwordInput.getBoundingClientRect();
        
        // Устанавливаем позицию подсказки
        tipContainer.style.position = 'absolute';
        tipContainer.style.top = `${inputRect.bottom + 5}px`;
        tipContainer.style.left = `${inputRect.left}px`;
        tipContainer.style.width = `${inputRect.width}px`;
        tipContainer.style.backgroundColor = '#f8f9fa';
        tipContainer.style.border = '1px solid #007bff';
        tipContainer.style.borderRadius = '8px';
        tipContainer.style.padding = '15px';
        tipContainer.style.boxShadow = '0 4px 12px rgba(0, 0, 0, 0.15)';
        tipContainer.style.zIndex = '1000';
        tipContainer.style.fontSize = '14px';
        tipContainer.style.color = '#333';

        // Текст подсказки
        const tipText = document.createElement('div');
        tipText.innerHTML = `
            <div style="margin-bottom: 10px; font-weight: 500;">💡 Требования к паролю:</div>
            <ul style="margin: 0; padding-left: 20px; font-size: 13px;">
                <li>Минимум 8 символов</li>
                <li>Заглавные и строчные буквы</li>
                <li>Хотя бы одна цифра</li>
                <li>Специальные символы</li>
            </ul>
        `;

        // Кнопка генерации пароля
        const generateButton = document.createElement('button');
        generateButton.textContent = '🎲 Сгенерировать надежный пароль';
        generateButton.style.marginTop = '10px';
        generateButton.style.padding = '8px 12px';
        generateButton.style.width = '100%';
        generateButton.style.backgroundColor = '#28a745';
        generateButton.style.color = 'white';
        generateButton.style.border = 'none';
        generateButton.style.borderRadius = '4px';
        generateButton.style.cursor = 'pointer';
        generateButton.style.fontSize = '12px';
        generateButton.style.transition = 'background-color 0.3s';
        
        generateButton.onmouseover = function() {
            this.style.backgroundColor = '#218838';
        };
        
        generateButton.onmouseout = function() {
            this.style.backgroundColor = '#28a745';
        };
        
        generateButton.onclick = function() {
            const newPassword = insertGeneratedPassword();
            
            // Показываем временное сообщение
            const message = document.createElement('div');
            message.textContent = '✅ Пароль сгенерирован и скопирован в оба поля!';
            message.style.marginTop = '8px';
            message.style.fontSize = '12px';
            message.style.color = '#28a745';
            message.style.fontWeight = '500';
            
            tipContainer.appendChild(message);
            
            // Удаляем сообщение через 3 секунды
            setTimeout(() => {
                if (message.parentNode) {
                    message.parentNode.removeChild(message);
                }
            }, 3000);
        };

        tipContainer.appendChild(tipText);
        tipContainer.appendChild(generateButton);
        document.body.appendChild(tipContainer);
    } else {
        // Если подсказка уже существует, показываем/скрываем её
        tipContainer.classList.toggle('active');
    }
}

function generateSecurePassword() {
    // Наборы символов
    const lowercase = 'abcdefghijklmnopqrstuvwxyz';
    const uppercase = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
    const numbers = '0123456789';
    const specialChars = '!@#$%^&*()_+-=[]{}|;:,.<>?';
    
    // Минимальные требования
    const minLength = 8;
    const targetLength = Math.floor(Math.random() * 5) + 12; // Длина от 12 до 16 символов
    
    let password = '';
    
    // Гарантируем наличие хотя бы одного символа из каждой категории
    password += lowercase[Math.floor(Math.random() * lowercase.length)];
    password += uppercase[Math.floor(Math.random() * uppercase.length)];
    password += numbers[Math.floor(Math.random() * numbers.length)];
    password += specialChars[Math.floor(Math.random() * specialChars.length)];
    
    // Все возможные символы для оставшейся части пароля
    const allChars = lowercase + uppercase + numbers + specialChars;
    
    // Добавляем остальные символы до нужной длины
    for (let i = password.length; i < targetLength; i++) {
        password += allChars[Math.floor(Math.random() * allChars.length)];
    }
    
    // Перемешиваем пароль для случайного порядка символов
    password = password.split('').sort(() => Math.random() - 0.5).join('');
    
    return password;
}

// Функция для вставки сгенерированного пароля в поле ввода
function insertGeneratedPassword() {
    const password = generateSecurePassword();
    const passwordInput = document.getElementById('passwordRegister');
    const confirmInput = document.getElementById('confirmPasswordRegister');
    
    if (passwordInput) {
        passwordInput.value = password;
    }
    
    if (confirmInput) {
        confirmInput.value = password;
    }
    
    return password;
}


/* Установка события по нажатии на кнопку Enter для всех форм */
document.addEventListener('DOMContentLoaded', function() {
    const authorizeForm = document.querySelector('.authorize-form');
    authorizeForm.addEventListener('keyup', function(event) {
        event.preventDefault();
        if (event.keyCode === 0x0D) { // Enter key is clicked
            AuthService.login();
        }
    });

    const registerForm = document.querySelector('.register-form');
    registerForm.addEventListener('keyup', function(event) {
        event.preventDefault();
        if (event.keyCode === 0x0D) { // Enter key is clicked
            AuthService.register();
        }
    });

    const recoveryForm = document.querySelector('.recovery-form');
    recoveryForm.addEventListener('keyup', function(event) {
        event.preventDefault();
        if (event.keyCode === 0x0D) { // Enter key is clicked
            AuthService.recover();
        }
    });
});

/* Установка событий для формы авторизации */
document.addEventListener('DOMContentLoaded', function() {
    const authInputs = document.querySelectorAll('.authorize-form input');
    const authOutputMessage = document.querySelector('.authorize-form .output-message');

    // Убираем сообщения при наведении на компонент
    function hideAuthErrorMessage() {
        AuthService.hideErrorMessage(authOutputMessage);
    }
    
    authInputs.forEach(input => {
        input.addEventListener('focus', hideAuthErrorMessage);
    });

    // Убираем надпись при нажатии на "Регистрация" или "Забыли пароль?"
    const registerLabel = document.getElementById('registerLabel');
    const forgotPasswordLabel = document.getElementById('forgotPasswordLabel');

    registerLabel.addEventListener('click', hideAuthErrorMessage);
    forgotPasswordLabel.addEventListener('click', hideAuthErrorMessage);
});


/* Установка событий для формы регистрации */
document.addEventListener('DOMContentLoaded', function() {
    const regInputs = document.querySelectorAll('.register-form input');
    const regOutputMessage = document.querySelector('.register-form .output-message');

    // Убираем сообщения при наведении на компонент
    function hideRegErrorMessage() {
        setTimeout(() => {
            AuthService.hideErrorMessage(regOutputMessage);
        }, 500);       
    }
    
    regInputs.forEach(input => {
        input.addEventListener('focus', hideRegErrorMessage);
    });

    // Убираем надписи при нажатии на "Вернуться назад"
    const backLabel = document.getElementById('registerBackLabel');
    const loginInput = document.getElementById('loginRegister');
    const emailInput = document.getElementById('emailRegister');
    const passwordInput = document.getElementById('passwordRegister');
    const confirmPasswordInput = document.getElementById('confirmPasswordRegister');

    backLabel.addEventListener('click', function() {
        setTimeout(() => {
            loginInput.value = '';
            emailInput.value = '';
            passwordInput.value = '';
            confirmPasswordInput.value = '';
        }, 500);     
    });
});

/* Установка событий для формы восстановления пароля */
document.addEventListener('DOMContentLoaded', function() {
    // Убираем содержимое input и message по возвращении назад
    const regInputs = document.getElementById('recoverEmail');
    const regOutputMessage = document.querySelector('.recovery-form .output-message');

    // Убираем надписи при нажатии на "Вернуться назад"
    const backLabel = document.getElementById('recoverBackLabel');

    backLabel.addEventListener('click', function() {
        setTimeout(() => {
            regInputs.value = '';
            AuthService.hideErrorMessage(regOutputMessage);
        }, 500);     
    });

    regInputs.addEventListener('focus', function() {
        setTimeout(() => {
            regInputs.value = '';
            AuthService.hideErrorMessage(regOutputMessage);
        });
    });
});

// Автоматическое переключение на нужную форму при заходе на страницу (из главной страницы)
document.addEventListener('DOMContentLoaded', function() {
    const hash = window.location.hash;

    if (hash === '#register') {
        showRegisterForm();
    }
});

// Добавляем обработчики для скрытия подсказки при потере фокуса
document.addEventListener('DOMContentLoaded', function() {
    const passwordInput = document.getElementById('passwordRegister');
    
    passwordInput.addEventListener('blur', function() {
        setTimeout(() => {
            const tipContainer = document.getElementById('generate-password-form');
            if (tipContainer) {
                tipContainer.classList.remove('active');
            }
        }, 200);
    });
    
    passwordInput.addEventListener('focus', function() {
        const tipContainer = document.getElementById('generate-password-form');
        if (tipContainer) {
            tipContainer.classList.add('active');
        }
    });
});
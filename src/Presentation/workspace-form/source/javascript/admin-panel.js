import { ApiService } from "./api.js";
import { getToken, getUserName, UserRights } from "./cookie.js";
import { fetchTableData } from "./form-service.js";
import { MessageBox } from "./form-utils.js";
import { TableGETSpecial, tableMap, TableName, TableVariables } from "./table-utils.js";
import { showTableData } from "./workspace-visuals.js";

window.switchTab = switchTab;
window.showUserForm = showUserForm;
window.editUser = editUser;
window.closeUserModal = closeUserModal;
window.addUser = addUser;
window.searchUsers = searchUsers;
window.showRoleForm = showRoleForm;
window.saveRole = saveRole;
window.registerShowGeneratePassword = registerShowGeneratePassword;
window.generate = generate;

// Словарь: наименование сущности -> Имя компонента пагинации
const paginationNameMap = new Map();
paginationNameMap.set(tableMap.get('Учетные записи'), 'usersPagination')
    .set(tableMap.get('Роли'), 'rolesPagination');

// Функции для вкладок
export async function switchTab(tabName) {
    // Получаем кодовую часть имени таблицы
    const tableCodeName = TableName.getCodeName(tabName);
    
    // Загрузить данные для вкладки
    switch (tabName) {
        case TableName.CREDENTIAL[0]: await loadUsers(); break;
        case TableName.ROLE[0]: await loadRoles(); break;
    }

    // Скрыть все вкладки
    document.querySelectorAll('.tab-content').forEach(tab => {
        tab.classList.remove('active');
    });
    
    // Убрать активный класс со всех кнопок
    document.querySelectorAll('.tab-btn').forEach(btn => {
        btn.classList.remove('active');
    });

    // Показать выбранную вкладку
    document.getElementById(`${tableCodeName}-tab`).classList.add('active');
    
    // Активировать кнопку
    if (event) {
        event.target.classList.add('active');
    } else {
        // Ищем кнопку по tabName и активируем её
        document.querySelectorAll('.tab-btn').forEach(btn => {
            if (btn.textContent === tabName || 
                btn.textContent.toLowerCase().includes(tabName.toLowerCase()) || 
                btn.getAttribute('data-tab') === tabName) {
                btn.classList.add('active');
            }
        });
    }
}

// Функции для пользователей
async function loadUsers() {
    
    // Смена переменных состояния текущей таблицы
    TableVariables.dataPage = 1;
    TableVariables.record = null;
    TableVariables.searchId = null;
    TableVariables.tableRUName = TableName.CREDENTIAL[0]; // Название таблицы - русское
    TableVariables.tableCodeName =  tableMap.get(TableVariables.tableRUName); // доступ к api

    const tableCodeName = TableName.getCodeName(TableVariables.tableRUName);

    await fetchTableData(TableVariables.tableRUName, TableVariables.tableCodeName, `${tableCodeName}Pagination`); // Загрузка данных
    showTableData(paginationNameMap.get(TableVariables.tableCodeName), `${tableCodeName}Table`, 
        `${tableCodeName}TableHead`, `${tableCodeName}TableBody`, `${tableCodeName}Info`);
}

// Функции для ролей
async function loadRoles() {
    // Смена переменных состояния текущей таблицы
    TableVariables.dataPage = 1;
    TableVariables.record = null;
    TableVariables.searchId = null;
    TableVariables.tableRUName = TableName.ROLE[0]; // Название таблицы - русское
    TableVariables.tableCodeName =  tableMap.get(TableVariables.tableRUName); // доступ к api

    const tableCodeName = TableName.getCodeName(TableVariables.tableRUName);

    await fetchTableData(TableVariables.tableRUName, TableVariables.tableCodeName, `${tableCodeName}Pagination`); // Загрузка данных
    showTableData(paginationNameMap.get(TableVariables.tableCodeName), `${tableCodeName}Table`, 
        `${tableCodeName}TableHead`, `${tableCodeName}TableBody`, `${tableCodeName}Info`);
}

// Модальные окна для пользователей
async function showUserForm() {
    try {
        const token = getToken();
        const data = await ApiService.get(`Role/`, {
            'Authorization': `Bearer ${token}`
        });

        document.getElementById('userModalTitle').textContent = 'Добавить пользователя';
        document.getElementById('userForm').reset();
        document.getElementById('password').required = true;
        document.getElementById('userModal').style.display = 'block';

        // Заполняем названия ролей
        const roleSelect = document.getElementById('roleId');
        roleSelect.innerHTML = '';
        
        data.forEach(set => {
            if (set.isDeleted !== null) return;

            const newOption = document.createElement('option');
            newOption.value = set.rights;
            newOption.textContent = set.forename;

            roleSelect.appendChild(newOption);
        });

        const passwordInputs = document.querySelectorAll('#password');

        passwordInputs.forEach(i => {
            i.addEventListener('blur', function() {
            
            setTimeout(() => {
                const tipContainer = document.getElementById('generate-password-form');
                if (tipContainer) {
                    tipContainer.classList.remove('active');
                }
            }, 200);
            });

            if (!i.id.includes('edit_password')) {
                i.classList.add('active');
            }
        });
        
        passwordInputs.forEach(i => i.addEventListener('focus', function() {
            const tipContainer = document.getElementById('generate-password-form');
            if (tipContainer) {
                tipContainer.classList.add('active');
            }
        }));

    } catch (error) {
        if (error.status === 401) {
            deleteUserData();
            window.location.href = '../../authorize-form/authorize.html';
            return;
        }

        console.error(error);
        
        MessageBox.ShowFromCenter(`Ошибка: ${error.data.message}`, 'red');
        return;
    }
}

export function registerShowGeneratePassword() {
    // Проверяем, не существует ли уже подсказки
    let tipContainer = document.getElementById('generate-password-form');
    
    if (!tipContainer) {
        tipContainer = document.createElement('div');
        tipContainer.id = 'generate-password-form';
        tipContainer.classList.add('generate-password-form', 'active');
        

        let passwordInput = document.getElementById('password');
        if (!passwordInput.classList.contains('active'))
            passwordInput = document.getElementById('edit_password')
        const inputRect = passwordInput.getBoundingClientRect();

        // Учитываем прокрутку страницы
        const scrollX = window.pageXOffset || document.documentElement.scrollLeft;
        const scrollY = window.pageYOffset || document.documentElement.scrollTop;
        
        // Устанавливаем позицию подсказки
        tipContainer.style.position = 'absolute';
        tipContainer.style.top = `${inputRect.bottom + scrollY + 5}px`;
        tipContainer.style.left = `${inputRect.left + scrollX}px`;
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

    let passwordInput = null;
    if (document.getElementById('userModal').style.display === 'block') {
        passwordInput = document.getElementById('password');
        const confirmInput = document.getElementById('confirmPassword');

        if (passwordInput) {
            passwordInput.value = password;
        }
        
        if (confirmInput) {
            confirmInput.value = password;
        }
    }
    else {
        passwordInput = document.getElementById('edit_password');
        if (passwordInput) {
            passwordInput.value = password;
        }
    }
    
    return password;
}

function editUser(userId) {
    // Здесь должна быть логика загрузки данных пользователя
    // Для демонстрации используем заглушку
    currentEditingUser = userId;
    document.getElementById('userModalTitle').textContent = 'Редактировать пользователя';
    document.getElementById('password').required = false;
    //loadRolesForSelect();
    
    // Загружаем данные пользователя (заглушка)
    document.getElementById('username').value = 'example_user';
    document.getElementById('email').value = 'user@example.com';
    document.getElementById('roleId').value = '1';
    document.getElementById('note').value = 'Пример примечания';
    
    document.getElementById('userModal').style.display = 'block';
}

function closeUserModal() {
    document.getElementById('userModal').style.display = 'none';
    document.getElementById('password').classList.remove('active');
}

async function addUser(event) {
    event.preventDefault();
    
    const formData = new FormData(event.target);
    if (formData.get('password') != formData.get('confirm-password')) {
        MessageBox.ShowFromCenter('Пароли не совпадают', 'red');
        return;
    }

    const roleInput = document.getElementById('roleId');
    const userData = {
        userName: formData.get('username'),
        email: formData.get('email'),
        password: formData.get('password'),
        whoRegister: getUserName(),
        registerRights: parseInt(roleInput.options[roleInput.selectedIndex].value)
    };
    
    MessageBox.ShowAwait();

    // Отправляем данные пользоваетеля
    try {
        const token = getToken();
        const data = await ApiService.post(`Credential/register`, userData, {
            'Authorization': `Bearer ${token}`
        });

        const newUser = await ApiService.get(TableGETSpecial.getByIdApiString(TableVariables.tableCodeName, data.id), {
            'Authorization': `Bearer ${token}`
        });

        MessageBox.RemoveAwait()

        closeUserModal();
        await MessageBox.ShowFromCenter('Пользователь успешно добавлен', 'green');
        
        TableVariables.tableData.push(newUser);

        const tableCodeName = TableName.CREDENTIAL[1];
        showTableData(paginationNameMap.get(TableVariables.tableCodeName), `${tableCodeName}Table`, 
            `${tableCodeName}TableHead`, `${tableCodeName}TableBody`, `${tableCodeName}Info`);
    } catch (error) {
        if (error.status === 401) {
            deleteUserData();
            window.location.href = '../../authorize-form/authorize.html';
            return;
        }
        
        await MessageBox.ShowFromCenter(`Ошибка: ${error.data.message}`);
        MessageBox.RemoveAwait();
        return;
    }
}

// Модальные окна для ролей
function showRoleForm() {
    document.getElementById('roleModalTitle').textContent = 'Добавить роль';
    document.getElementById('roleForm').reset();
    document.getElementById('roleModal').style.display = 'block';
}

window.closeRoleModal = function closeRoleModal() {
    document.getElementById('roleModal').style.display = 'none';
}

async function saveRole(event) {
    event.preventDefault();
    
    const formData = new FormData(event.target);
    const rights = parseInt(formData.get('rights'));
    const roleData = {
        Forename: formData.get('forename'),
        Rights: rights,
        CanGet: true,
        CanPost: rights !== UserRights.Basic && rights !== UserRights.Director,
        CanUpdate: rights !== UserRights.Basic && rights !== UserRights.Director,
        CanDelete: rights === UserRights.Admin,
        whoAdded: getUserName()
    };
    
    try {
        const token = getToken('token');
        
        const data = await ApiService.post(`Role/`, roleData, {
            'Authorization': `Bearer ${token}`
        })
        
        closeRoleModal();
        MessageBox.ShowFromCenter('Роль успешно добавлена', 'green');
        
        const newRole = await ApiService.get(`Role/${data.id}`, {
            'Authorization': `Bearer ${token}`
        });

        TableVariables.tableData.push(newRole);

        const tableCodeName = TableName.ROLE[1];
        showTableData(paginationNameMap.get(TableVariables.tableCodeName), `${tableCodeName}Table`, 
            `${tableCodeName}TableHead`, `${tableCodeName}TableBody`, `${tableCodeName}Info`);
        
    } catch (error) {
        if (error.status === 401) {
            deleteUserData();
            window.location.href = '../../authorize-form/authorize.html';
            return;
        }

        MessageBox.ShowFromCenter('Ошибка сохранения роли', 'red');
    }
}

async function generate(entity) {
    const entityCountInput = document.getElementById('entityCount');
    if (entityCountInput.value === '') {
        MessageBox.ShowFromCenter('Введите количество записей', 'red');
        return;
    }

    const entityCount = parseInt(entityCountInput.value);

    if (entityCount <= 0) {
        MessageBox.ShowFromCenter('Число записей должно быть положительным числом', 'red');
        return;
    }

    MessageBox.ShowAwait();

    try {
        const data = await ApiService.get(`Generator/${entity}?count=${entityCount}`, {
            'Authorization': `Bearer ${getToken()}`
        });

        await MessageBox.ShowFromCenter('Генерация прошла успешно', 'green');

        entityCountInput.value = '';
    } catch (error) {
        await MessageBox.ShowFromCenter(`Ошибка: ${error.data.message}`, 'red');
    } finally {
        await MessageBox.RemoveAwait();
    }
}

// Поиск
function searchUsers() {
    const searchTerm = document.getElementById('userSearch').value.toLowerCase();
    const rows = document.querySelectorAll('#usersTableBody tr');
    
    rows.forEach(row => {
        const username = row.cells[2].textContent.toLowerCase();
        const email = row.cells[4].textContent.toLowerCase();
        
        if (username.includes(searchTerm) || email.includes(searchTerm)) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });
}
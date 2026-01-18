import { fetchTableData, setupPagination, detectFieldType} from "./form-service.js";
import { formatValue, getCellClassName, getCurrentPageData, checkDatabaseAccess } from "./database-general-service.js";
import { TableModifying } from "./table-service.js";
import { getUserRights, UserRights } from "./cookie.js";
import { DATA_PER_PAGE, fieldNameMapping, TableAction, tableMap, TableVariables } from "./table-utils.js";
import { showTableData } from "./workspace-visuals.js";
import { MessageBox } from "./form-utils.js";

window.loadTableData = loadTableData;
window.changePage = changePage;
window.closeRecordModalForm = closeRecordModalForm;
window.searchSelectChange = searchSelectChange;
window.searchInputChange = searchInputChange;
window.clearSearch = clearSearch;

// Загрузка данных в таблицу
export async function loadTableData(useCache = true) {
    MessageBox.ShowAwait();
    
    const tableSelect = document.getElementById('tableSelect');
    
    const currentTable = tableSelect.value;
    
    hideTableInterface();
    
    // Сбрасываем поиск при смене таблицы
    clearSearch('dataPagination', 'dataTable', 'dataTableHead', 'dataTableHead', 'dataInfo');

    TableVariables.tableRUName = currentTable !== 'default' ? currentTable : null;
    TableVariables.tableCodeName = currentTable !== 'default' ? tableMap.get(currentTable) : null;

    if (currentTable === '') {
        tableSelect.selectedIndex = 0;
        document.getElementById('dataTable').style.display = 'none';
        document.getElementById('dataInfo').style.display = 'none';
        document.getElementById('dataPagination').style.display = 'none';
        document.getElementById('addRecordBtn').style.display = 'none';
        for (const e of document.getElementsByClassName('search-controls')) {
            e.style.display = 'none';
        }

        MessageBox.RemoveAwait();
        return;
    }
    
    // Загружаем данные таблицы
    try {
        await fetchTableData(TableVariables.tableRUName, TableVariables.tableCodeName, useCache);
    } catch (error) {
        MessageBox.RemoveAwait();
        return;
    }

    // Проверяем права доступа
    checkDatabaseAccess();

    showTableData('dataPagination', 'dataTable', 'dataTableHead', 'dataTableBody',
        'dataInfo'); // обновляем отображение страницы

    // Заполняем поле для поиска
    const searchSelect = document.getElementById('searchSelect');
    searchSelect.innerHTML = '';
    
    const field = [];
    document.querySelectorAll('#dataTable th').forEach(h => {
        const newOption = document.createElement('option');
        const dataField = h.getAttribute('data-field');
        if (!field.includes(dataField) && dataField !== 'actions') {
            field.push(dataField);
            newOption.value = dataField;
            newOption.textContent = fieldNameMapping[dataField];
            searchSelect.appendChild(newOption);
        }
    });

    document.getElementById('searchById').setAttribute('placeholder', `Введите \"${searchSelect.options[searchSelect.selectedIndex].text}\"`);
    document.getElementById('addRecordBtn').style.display = 'flex';

    MessageBox.RemoveAwait();
}

// Сокрытие интерфейса таблиц
export function hideTableInterface() {
    document.querySelectorAll('.table-info').forEach(e => e.style.display = 'none');  
    document.querySelectorAll('.-table').forEach(e => e.style.display = 'none');
    document.querySelectorAll('.pagination').forEach(e => e.style.display = 'none');  
    document.querySelectorAll('.search-results-info').forEach(e => e.style.display = 'none');  
}

// Отображение данных таблицы
export async function displayTableData(data, paginationID, tableID, tableHeadID, tableBodyID, tableInfoID, tableRUName, tableCodeName) {   
    const tableHead = document.getElementById(tableHeadID);
    const tableBody = document.getElementById(tableBodyID);
    const tableInfo = document.getElementById(tableInfoID);
    const dataTable = document.getElementById(tableID);
    const pagination = document.getElementById(paginationID);

    const userRights = getUserRights();
    
    // Очищаем таблицу
    tableHead.innerHTML = '';
    tableBody.innerHTML = '';
    
    if (!data || data.length === 0) {
        dataTable.style.display = 'none';
        pagination.style.display = 'none';
        
        tableInfo.style.display = 'none';
        return;
    }
    
    // Показываем элементы интерфейса
    dataTable.style.display = 'table';
    tableInfo.style.display = 'flex';
    
    // Заполняем информацию о таблице
    document.querySelectorAll('.text-table-info').forEach(e => e.textContent = tableRUName);
    
    // ИСПРАВЛЕНИЕ: Всегда показываем общее количество записей и текущий диапазон
    const totalRecords = TableVariables.tableData.length;
    const startRecord = (TableVariables.dataPage - 1) * DATA_PER_PAGE + 1;
    const endRecord = Math.min(TableVariables.dataPage * DATA_PER_PAGE, totalRecords);
    
    if (TableVariables.searchId) {
        // В режиме поиска показываем информацию о найденной записи
        document.querySelectorAll('.record-count').forEach(e => e.textContent = `Найдена 1 запись из ${totalRecords}`);
    } else {
        // В обычном режиме показываем диапазон и общее количество
        document.querySelectorAll('.record-count').forEach(e => e.textContent = `Записи: ${startRecord}-${endRecord} из ${totalRecords}`);
    }
    
    // Создаем заголовки таблицы ДИНАМИЧЕСКИ из первого объекта массива
    const headerRow = document.createElement('tr');
    const dataKeys = Object.keys(data[0]);
    
    // Создаем заголовки для каждого ключа данных
    dataKeys.forEach(key => {
        
        // Не отображаем графу ID для базового пользователя
        if (userRights === UserRights.Basic && (key === 'id' || key === 'isDeleted')) {
            return;
        }

        const th = document.createElement('th');
        
        // Используем маппинг для русских названий или преобразуем ключ
        if (fieldNameMapping[key]) {
            th.textContent = fieldNameMapping[key];
        } else {
            // Автоматическое преобразование
            th.textContent = key
                .replace(/_/g, ' ')
                .replace(/([A-Z])/g, ' $1')
                .replace(/^./, str => str.toUpperCase())
                .trim();
        }
        
        th.style.textAlign = 'center';
        th.setAttribute('data-field', key);
        headerRow.appendChild(th);
    });
    
    // Добавляем столбец для действий если есть права
    if (userRights != UserRights.Basic) {
        const actionsTh = document.createElement('th');
        actionsTh.textContent = 'Действия';
        actionsTh.style.textAlign = 'center';
        actionsTh.setAttribute('data-field', 'actions');
        headerRow.appendChild(actionsTh);
    }
    
    tableHead.appendChild(headerRow);
    
    // Заполняем данные
    data.forEach((record) => {
        const row = document.createElement('tr');
        if (TableVariables.searchId && record.id === TableVariables.searchId) {
            row.classList.add('search-highlight');
        }
        
        // Проходим по всем ключам объекта
        dataKeys.forEach(key => {

            // Не отображаем графу ID для базового пользователя
            if (userRights === UserRights.Basic && (key === 'id' || key === 'isDeleted')) {
                return;
            }

            const td = document.createElement('td');
            const value = record[key];
            
            // Определяем тип поля для форматирования
            const fieldType = detectFieldType(key, value);
            
            // Форматируем значение
            td.textContent = formatValue(value, fieldType);
            td.style.textAlign = 'center';
            td.className = getCellClassName(fieldType, value);
            td.setAttribute('data-field', key);
            
            row.appendChild(td);
        });
        
        // Добавляем кнопки действий если есть права
        if (userRights != UserRights.Basic) {
            const actionsTd = document.createElement('td');
            actionsTd.className = 'table-actions';
            actionsTd.setAttribute('data-field', 'actions');
            
            if (userRights === UserRights.Editor || userRights === UserRights.Admin) {
                const editBtn = document.createElement('button');
                editBtn.className = 'btn-edit-small';
                editBtn.innerHTML = '✏️';
                editBtn.title = 'Редактировать';
                editBtn.onclick = async () => await TableModifying(record, TableAction.Edit, tableRUName);

                actionsTd.appendChild(editBtn);
            }
             
            if (userRights === UserRights.Admin) {
                const deleteBtn = document.createElement('button');
                deleteBtn.className = 'btn-delete-small';
                deleteBtn.innerHTML = '🗑️';
                deleteBtn.title = 'Удалить';
                deleteBtn.onclick = async () => await TableModifying(record, TableAction.Delete, tableRUName);

                const recoverBtn = document.createElement('button');
                recoverBtn.className = 'btn-recover-small';
                recoverBtn.innerHTML = '🔄';
                recoverBtn.title = 'Восстановить';
                recoverBtn.onclick = async () => await TableModifying(record, TableAction.Recover, tableRUName);      

                actionsTd.appendChild(deleteBtn);
                actionsTd.appendChild(recoverBtn);
            }
            
            row.appendChild(actionsTd);
        }
        
        tableBody.appendChild(row);
    });
    
    // Настраиваем пагинацию
    setupPagination(paginationID);
}

// Отображение результатов поиска
export function displaySearchResults(results) {
    const tableBody = document.getElementById('dataTableBody');
    const dataTable = document.getElementById('dataTable');

    const userRights = getUserRights();
       
    if (!results || results.length === 0) {
        dataTable.style.display = 'none';
        return;
    }
    
    // Показываем таблицу
    dataTable.style.display = 'table';
    
    // Очищаем таблицу
    tableBody.innerHTML = '';
    
    // Заполняем данными
    const dataKeys = Object.keys(results[0]);

    const startRecord = (TableVariables.dataPage - 1) * DATA_PER_PAGE + 1;
    const endRecord = Math.min(TableVariables.dataPage * DATA_PER_PAGE, TableVariables.searchResults.length);

    const pageRecords = results.slice(startRecord, endRecord);
    
    pageRecords.forEach((record) => {
        const row = document.createElement('tr');
        row.classList.add('search-highlight');
        
        dataKeys.forEach(key => {
            const td = document.createElement('td');
            const value = record[key];
            const fieldType = detectFieldType(key, value);
            
            td.textContent = formatValue(value, fieldType);
            td.className = getCellClassName(fieldType, value);
            td.setAttribute('data-field', key);
            
            // Особо выделяем ячейку с ID
            if (key === 'id' && value === TableVariables.searchId) {
                td.style.fontWeight = 'bold';
                td.style.color = '#667eea';
            }
            
            row.appendChild(td);
        });
        
        // Добавляем кнопки действий если есть права
        if (userRights != UserRights.Basic) {
            const actionsTd = document.createElement('td');
            actionsTd.className = 'table-actions';
            actionsTd.setAttribute('data-field', 'actions');
            
            if (userRights === UserRights.Editor || userRights === UserRights.Admin) {
                const editBtn = document.createElement('button');
                editBtn.className = 'btn-edit-small';
                editBtn.innerHTML = '✏️';
                editBtn.title = 'Редактировать';
                editBtn.onclick = async () => await TableModifying(record, TableAction.Edit);

                actionsTd.appendChild(editBtn);
            }
             
            if (userRights === UserRights.Admin) {
                const deleteBtn = document.createElement('button');
                deleteBtn.className = 'btn-delete-small';
                deleteBtn.innerHTML = '🗑️';
                deleteBtn.title = 'Удалить';
                deleteBtn.onclick = () => confirmDeleteRecord(record);

                actionsTd.appendChild(deleteBtn);
            }
            
            row.appendChild(actionsTd);
        }
        
        tableBody.appendChild(row);
    });

    TableVariables.searchResults = results;

    setupPagination('dataPagination');
}

// Показать сообщение о ненайденных результатах
export function showNoSearchResults() {
    const dataTable = document.getElementById('dataTable');
    const noSearchResultsMessage = document.getElementById('noSearchResultsMessage');
    const searchResultsInfo = document.getElementById('searchResultsInfo');
    const searchResultsText = document.getElementById('searchResultsText');
    
    // Скрываем таблицу и показываем сообщение
    dataTable.style.display = 'none';
    noSearchResultsMessage.style.display = 'block';
    
    // Показываем информацию о поиске
    searchResultsText.textContent = `Запись с ID: ${TableVariables.searchId} не найдена`;
    searchResultsInfo.style.display = 'block';
    
    // ИСПРАВЛЕНИЕ: Показываем общее количество записей
    const totalRecords = TableVariables.tableData.length;
    document.getElementById('recordCount').textContent = `Записей: 0 из ${totalRecords}`;
}

// Очистка поиска
export function clearSearch(paginationID, tableID, tableHeadID, tableBodyID, tableInfoID) {
    TableVariables.searchId = null;
    TableVariables.searchResults = null;
    
    // Сбрасываем поле поиска
    document.getElementById('searchById').value = '';
    document.getElementById('clearSearchBtn').style.display = 'none';
    
    // Сбрасываем на первую страницу и показываем все данные
    TableVariables.dataPage = 1;
    if (TableVariables.tableData && TableVariables.tableData.length > 0) {
        showTableData(paginationID, tableID, tableHeadID, tableBodyID, tableInfoID);
        document.getElementById('databaseRecordCount').style.display = 'flex';
    }
}

// Смена страницы
async function changePage(page, paginationID, tableID, tableHeadID, tableBodyID, tableInfoID) {
    TableVariables.dataPage = page;
    await displayTableData(getCurrentPageData(TableVariables.searchResults), paginationID, tableID, tableHeadID, tableBodyID, tableInfoID, 
        TableVariables.tableRUName, TableVariables.tableCodeName);
    
    // Прокрутка к верху таблицы
    const tableContainer = document.querySelector('.table-container');
    if (tableContainer) {
        tableContainer.scrollIntoView({ 
            behavior: 'smooth', 
            block: 'start' 
        });
    }
}

function searchSelectChange() {
    // Заполняем поле для поиска
    const searchSelect = document.getElementById('searchSelect');
    document.getElementById('searchById').setAttribute('placeholder', `Введите \"${searchSelect.options[searchSelect.selectedIndex].text}\"`);searchByIdBtn
}

export function searchInputChange() {    
    const searchSelect = document.getElementById('searchSelect');
    const key = searchSelect.options[searchSelect.selectedIndex].value;

    const searchInput = document.getElementById('searchById');
    const text = searchInput.value.trim();
    
    // Если поиск пустой, показываем все записи или сбрасываем поиск
    if (text === '') {
        TableVariables.searchResults = null;
        TableVariables.dataPage = 1;
        document.getElementById('databaseRecordCount').style.display = 'flex';
        showTableData('dataPagination', 'dataTable', 'dataTableHead', 'dataTableBody', 'dataInfo');
        return;
    }

    MessageBox.ShowAwait();
    
    // Ищем записи по подстроке в указанном поле
    updateSearchResults(key, text);

    // Проверяем длину массива, а не сам массив
    if (!TableVariables.searchResults || TableVariables.searchResults.length === 0) {
        displaySearchResults(TableVariables.searchResults); // убрали лишние []
        document.getElementById('dataPagination').style.display = 'none';
    } else {
        displaySearchResults(TableVariables.searchResults); // убрали лишние []
        changePage(1, 'dataPagination', 'dataTable', 'dataTableHead', 'dataTableBody', 'dataInfo');
    }
    
    // Показываем кнопку очистки
    document.getElementById('clearSearchBtn').style.display = 'inline-block';
    document.getElementById('databaseRecordCount').style.display = 'none';

    MessageBox.RemoveAwait();
}

export function updateSearchResults(key, text) {
    const foundRecords = [];
    for (const record of TableVariables.tableData) {
        // Преобразуем значение в строку и ищем подстроку (регистронезависимо)
        const fieldValue = String(record[key] || '').toLowerCase();
        const searchText = text.toLowerCase();
        
        if (fieldValue.includes(searchText)) {
            foundRecords.push(record);
        }
    }

    TableVariables.searchResults = foundRecords.length === 0 ? null : foundRecords;
    return TableVariables.searchResults;
}


// Инициализация при загрузке страницы
document.addEventListener('DOMContentLoaded', function() {
    // Добавляем обработчик Enter для поля поиска
    const searchInput = document.getElementById('searchById');
    if (searchInput) {
        searchInput.addEventListener('keypress', function (event) {
            if (event.key === 'Enter') {
                searchInputChange();
            }
    });
    }
});

// Закрытие по клику вне модального окна
window.addEventListener('click', function(event) {
    const editModal = document.getElementById('editRecordModal');
    if (event.target === editModal) {
        closeRecordModalForm();
    }
});

// Предотвращаем закрытие при клике на само модальное окно
document.addEventListener('click', function(event) {
    const modalContent = document.querySelector('#editRecordModal .modal-content');
    if (modalContent && modalContent.contains(event.target)) {
        event.stopPropagation();
    }
});



// Закрытие модального окна
export function closeRecordModalForm() {
    document.getElementById('editRecordModal').style.display = 'none';
    TableVariables.record = null;
    
    // Разблокируем скролл body
    document.body.classList.remove('modal-open');
    
    // Очищаем форму
    document.getElementById('editRecordFields').innerHTML = '';
}
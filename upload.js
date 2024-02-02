/* 
    Long Method
    มีกระบวนการมากเกินไปภายในฟังก์ชันเดียว และซับซ้อน
    ทั้งอ่านไฟล์ XLSX และสร้างตาราง HTML ซึ่งจะทำให้อ่าน ทำความเข้าใจ ได้ยาก
*/

const xlsxFileInput = document.getElementById('xlsxFile');
const tableBody = document.querySelector('#excel-table tbody');

xlsxFileInput.addEventListener('change', handleFileChange);

async function handleFileChange(event) {

    const file = event.target.files[0];
    try {
        const rows = await readXlsxFile(file);
        renderTableData(rows.slice(1));
        showPreview();
    } catch (error) {
        console.error('Error reading XLSX file:', error);
    }

}

function renderTableData(rows) {
    rows.forEach(renderRow);
}

function renderRow(row) {
    const rowElement = document.createElement('tr');
    row.forEach(cellValue => {
        const cellElement = document.createElement('td');
        cellElement.textContent = cellValue;
        rowElement.appendChild(cellElement);
    });
    tableBody.appendChild(rowElement);
}

function showPreview() {
    document.getElementById('excel-preview').classList.remove('d-none');
}
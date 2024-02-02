/*var input = document.getElementById('xlsxFile');
var table = document.getElementById('excel-table').querySelector('tbody');

input.addEventListener('change', (event) => {
    var file = event.target.files[0];

    readXlsxFile(file).then((rows) => {
        table.innerHTML = "";

        for (var row = 1; row < rows.length; row++) {
            var rowElement = document.createElement('tr');

            for (var cellIndex = 0; cellIndex < rows[row].length; cellIndex++) {
                var cellElement = document.createElement('td');
                cellElement.textContent = rows[row][cellIndex];
                rowElement.appendChild(cellElement);
            }

            table.appendChild(rowElement);
        }

        document.getElementById('excel-preview').classList.remove('d-none');
    });
});*/

/* 
    Long Method
    มีกระบวนการมากเกินไปภายในฟังก์ชันเดียว 
    ทั้งอ่านไฟล์ XLSX และสร้างตาราง HTML ซึ่งจะทำให้อ่าน ทำความเข้าใจ ได้ยาก
*/

var xlsxFile = document.getElementById('xlsxFile');
var table = document.getElementById('excel-table').querySelector('tbody');

xlsxFile.addEventListener('change', (event) => {
    var file = event.target.files[0];

    readXlsxFile(file).then((rows) => {
        table.innerHTML = "";

        for (var row = 1; row < rows.length; row++) {
            addRowToTable(rows, row)
        }

        document.getElementById('excel-preview').classList.remove('d-none');
    });
});

function addRowToTable(rows, row) {
    var rowElement = document.createElement('tr');
    for (var cellIndex = 0; cellIndex < rows[row].length; cellIndex++) {
        var cellElement = document.createElement('td');
        cellElement.textContent = rows[row][cellIndex];
        rowElement.appendChild(cellElement);
    }
    table.appendChild(rowElement);
}

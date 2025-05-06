/**
 * スプレッドシートを開くときに。カスタムメニューを追加する関数
 */
function onOpen() {
    var spreadsheet = SpreadsheetApp.getActiveSpreadsheet();
    var entries = [
        {
            name: "マスタデータを生成",
            functionName: "showDialog"
        }
    ];
    spreadsheet.addMenu("Make", entries);
}

/**
 * ダイアログを表示する関数
 * - make_dialog.htmlを読み込んで、表示をする
 */
function showDialog() {
    var dlHtml = HtmlService.createTemplateFromFile("make_dialog").evaluate();
    SpreadsheetApp.getUi().showModalDialog(dlHtml, "マスタデータをダウンロード");
}

/**
 * アクティブなシートの名前を取得する関数
 * @returns {string} シート名
 */
function getSheetName() {
    return SpreadsheetApp.getActiveSpreadsheet().getActiveSheet().getName();
}

/**
 * マスタデータを生成しJSON形式で返す関数
 * @returns {string} 圧縮されたJSONデータ文字列
 */
function make(format) {
    var sheet = SpreadsheetApp.getActiveSheet();
    var maxRow = sheet.getLastRow();
    var maxColumn = sheet.getLastColumn();

    var typeRow = 2; // 2行目にデータ型
    var keysRow = 3; // 3行目に変数名
    var dataStartRow = 4; // 4行目からデータ開始

    var keys = [];
    var types = [];
    var jsonData = [];
    var ignoredColumns = [];

    // キー名と型の処理
    for (var x = 1; x <= maxColumn; x++) {
        var type = sheet.getRange(typeRow, x).getValue().toString().toLowerCase(); // **型を取得**
        var key = sheet.getRange(keysRow, x).getValue().toString(); // **キー名を取得**

        if (key.toLowerCase() === "ignore") {
            ignoredColumns.push(x);
            continue;
        }

        keys.push(key.charAt(0).toLowerCase() + key.slice(1));
        types.push(type); // **型をリストに追加**
    }

    // データの処理
    for (var y = dataStartRow; y <= maxRow; y++) {
        var json = {};
        for (var x = 1; x <= maxColumn; x++) {
            if (!ignoredColumns.includes(x)) {
                var data = sheet.getRange(y, x).getValue();
                try {
                    data = processDataByType(data, types[x - 1]);
                } catch (error) {
                    Logger.log("エラー: " + error.message);
                    continue;
                }
                json[keys[x - 1]] = data;
            }
        }
        jsonData.push(json);
    }

    if (format === "release") {
        return JSON.stringify({ data: jsonData });
    } else if (format === "csharpMakefile") {
        return JSON.stringify({ className: sheet.getName(), types: types, columns: keys }, null, '\t');
    } else {
        return JSON.stringify({ className: sheet.getName(), types: types, columns: keys, data: jsonData }, null, '\t');
    }
}

/**
 * データ型に基づいてデータを変換する関数
 * @param {*} data 変換対象のデータ
 * @param {string} type データ型 ("string", "bool" など)
 * @returns {*} 型に応じて変換されたデータ
 */
function processDataByType(data, type) {
    if (type == "string") {
        return data.toString();
    }
    if (type == "bool") {
        return !!data; // 数値や文字列をbooleanに変換
    }
    return data; // その他の型はそのまま返す
}
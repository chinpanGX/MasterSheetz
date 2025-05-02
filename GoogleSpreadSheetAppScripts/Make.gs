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

    var startRow = 2;
    var keysRow = startRow;
    var dataStartRow = startRow + 1;

    var keys = [];
    var dataType = [];
    var jsonData = [];
    var ignoredColumns = [];

    for (var x = 1; x <= maxColumn; x++) {
        var key = sheet.getRange(keysRow, x).getValue().toString();
        if (key.toLowerCase() == "ignore") {
            ignoredColumns.push(x);
            continue;
        }
        keys.push(key.charAt(0).toLowerCase() + key.slice(1));
    }

    for (var x = 1; x <= maxColumn; x++) {
        if (!ignoredColumns.includes(x)) {
            dataType.push(sheet.getRange(keysRow, x).getValue().toString());
        }
    }

    for (var y = dataStartRow; y <= maxRow; y++) {
        var json = {};
        for (var x = 1; x <= maxColumn; x++) {
            if (!ignoredColumns.includes(x)) {
                var data = sheet.getRange(y, x).getValue();
                try {
                    data = processDataByType(data, dataType[x - 1]);
                } catch (error) {
                    Logger.log("エラー: " + error.message);
                    continue;
                }
                json[keys[x - 1]] = data;
            }
        }
        jsonData.push(json);
    }

    var jsonString = FormatJson(jsonData);

    // リクエストに応じて出力形式を変更
    if (format === "bytes") {
        var binaryData = Utilities.newBlob(jsonString, "application/octet-stream").getBytes();
        return Utilities.base64Encode(binaryData);
    } else { // JSON形式で出力
        return jsonString;
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

/**
 * JSONデータを整形する
 * @param {Array} jsonData JSONデータの配列
 * @returns {string} rootをつけて、改行とタブを削除したJSONデータ
 */
function FormatJson(jsonData) {
    // rootをつけて、jsonUtilityで読み込めるようにする
    var formattedJson = JSON.stringify({ root: jsonData }, null, '\t');
    return formattedJson.replace(/\n/g, "").replace(/\r/g, "").replace(/\t/g, "");
}
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
function make() {
    var sheet = SpreadsheetApp.getActiveSheet();
    var maxRow = sheet.getLastRow();
    var maxColumn = sheet.getLastColumn();

    // 開始する行定義
    var startRow = 2;
    var keysRow = startRow; // キーが含まれる行
    var dataStartRow = startRow + 1; // 実際のデータが開始する行

    var keys = [];
    var dataType = [];
    var jsonData = [];
    var ignoredColumns = []; // 無視するカラム

    // キーの処理
    for (var x = 1; x <= maxColumn; x++) {
        var key = sheet.getRange(keysRow, x).getValue().toString();
        if (key.toLowerCase() == "ignore") {
            ignoredColumns.push(x);
            continue; // 無視するカラムの処理をスキップ
        }
        keys.push(key.charAt(0).toLowerCase() + key.slice(1));
    }

    // データ型の処理
    for (var x = 1; x <= maxColumn; x++) {
        if (!ignoredColumns.includes(x)) {
            dataType.push(sheet.getRange(keysRow, x).getValue().toString());
        }
    }

    // データの処理
    for (var y = dataStartRow; y <= maxRow; y++) {
        var json = {};
        for (var x = 1; x <= maxColumn; x++) {
            if (!ignoredColumns.includes(x)) {
                var data = sheet.getRange(y, x).getValue();
                try {
                  data = processDataByType(data, dataType[x - 1])
                }
                catch {
                  Logger.log("エラー: " + error.message); // ログにエラーメッセージを出力
                  continue; // エラー発生時はそのデータをスキップ
                }
                json[keys[x - 1]] = data;
            }
        }
        jsonData.push(json);
    }

    return FormatJson(jsonData)
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
 * @returns {string} 整形済みかつ簡略化されたJSON文字列
 */
function FormatJson(jsonData) {
    // rootをつけて、jsonUtiltityで読み込めるようにする
    var formattedJson = JSON.stringify({ root: jsonData }, null, '\t');
    // 改行やスペースを削除して1行にする
    return formattedJson.replace(/\n/g, "").replace(/\r/g, "").replace(/\t/g, "").replace(/ /g, "");
}

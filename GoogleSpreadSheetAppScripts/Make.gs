function showDialog() {
  // ダイアログテンプレート読み込み
  var dl_html = HtmlService.createTemplateFromFile("download_dialog").evaluate();
  // ダイアログ表示
  SpreadsheetApp.getUi().showModalDialog(dl_html, "マスタデータをダウンロード");
}

function getSheetName() {
  return SpreadsheetApp.getActiveSpreadsheet().getActiveSheet().getName();
}

function onOpen() {
  var spreadsheet = SpreadsheetApp.getActiveSpreadsheet();
  var entries = [{
    name : "マスタデータを生成",
    functionName : "showDialog"
  }];
  spreadsheet.addMenu("Make", entries);
}

function make() {
  var sheet = SpreadsheetApp.getActiveSheet();
  var maxRow = sheet.getLastRow();
  var maxColumn = sheet.getLastColumn();

  var keys = [];
  var dataType = [];
  var jsonData = [];
  var ignoreColumn = [];

  // キーの処理をする
  for (var x = 1; x <= maxColumn; x++) {
    var key = sheet.getRange(2, x).getValue().toString();
    // 1行目を無視する
    if (key.slice(0, 1) == "_") {
      ignoreColumn.push(x);
    }
    var convertKey = key.slice(0, 1).toLowerCase() + key.slice(1);
    keys.push(convertKey);
  }

  for (var x = 1; x <= maxColumn; x++) {
    var type = sheet.getRange(2, x).getValue().toString();
    dataType.push(type);
  }

  // データの処理をする
  for (var y = 3; y <= maxRow; y++) {
    var json = {};
    for (var x = 1; x <= maxColumn; x++) {
      if (ignoreColumn.includes(x)) continue;
      var data = sheet.getRange(y, x).getValue();
      if (dataType[x - 1] == "string") {
        data = data.toString();
      }
      if (dataType[x - 1] == "bool") {
        data = data ? 1 : 0;
      }
      json[keys[x - 1]] = data;
    }
    jsonData.push(json);
  }

  // JsonUtilityで読み込めるように, rootを追加する
  var outputJson = {
    Root: jsonData
  };

  outputJson = JSON.stringify(outputJson, null, '\t');
  // 1行にして出力する
  outputJson = outputJson.replace(/\n/g, "").replace(/\r/g, "").replace(/\t/g, "").replace(/ /g, "");
  return outputJson;
}

# get-pleasanter-site

`get-pleasanter-site`はPleasanterからサイト設定を取得するコマンドラインツールです。

## 機能

- Pleasanterのサイト構成を取得してローカル保存します。

## 使用方法

### 構成取得

Pleasanterのスクリプトを送信するには、以下のコマンドを使用します。

```sh
dotnet get-pleasanter-site.dll PutScript <ConfigurationFileName>
```
ConfigurationFileNameには、アップロードに関する設定情報が記載されたXMLファイルのパスを指定します。

### デフォルト設定ファイルの取得
設定ファイルをのひな形となる、デフォルトの設定ファイルを取得するには、以下のコマンドを使用します。
このコマンドを使用するとデフォルトパラメータが指定された設定ファイルが出力されます。
OutFileNameには、出力されるデフォルトの設定ファイル名称を指定します。

```
dotnet get-pleasanter-site.dll DefaultConfiguration <OutFileName>
```
設定ファイルの出力例は以下となります。

```
<?xml version="1.0" encoding="utf-8"?>
<RunConfiguration xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <SaveFile>保存するときのファイル名</SaveFile>
  <ApiKey>PleasanterのAPIキーを指定してください。</ApiKey>
  <PleasanterURL>PleasanterのURLを指定してください。</PleasanterURL>
  <SiteID>-1</SiteID>
</RunConfiguration>

```

設定ファイルにおける各種項目の詳細は以下となります。

| 要素名                   | 説明                                                                                              | 例                                                                                        |
|------------------------|-------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------|
| `ApiKey`               | PleasanterのAPIキーを指定します。これはPleasanterへの認証に使用されます。                           | `<ApiKey>your_api_key_here</ApiKey>`                                                      |
| `PleasanterURL`        | PleasanterのベースURLを指定します。これはスクリプト送信先のURLです。                                | `<PleasanterURL>http://example.com</PleasanterURL>`                                       |
| `SiteID`               | 構成を取得するPleasanter上のサイトIDを指定します。                                          | `<SiteID>1</SiteID>`                                                                      |
| `SaveFile`               | 保存するファイル名を指定します。                                          | `<SaveFile>export.json</SaveFile>`                                                                      |


## ライセンス

このプロジェクトはMITライセンスのもとで公開されています。
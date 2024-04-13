using System;
using GetPleasanterSite.Models;
using System.Text;
using System.Text.Json;
using GetPleasanterSite.Service.JSONProcessing;

namespace GetPleasanterSite.Configuration
{

    public interface ITrim
    {
        void doTrim();
    }

    public class RunConfiguration : ITrim
    {
        /// <summary>
        ///  接続用のAPIキー
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;


        /// <summary>
        /// プリザンターのURL
        /// </summary>
        public string PleasanterURL { get; set; } = string.Empty;


        /// <summary>
        /// 取得対象のサイトID
        /// </summary>
        public long SiteID { get; set; } = -1;

        /// <summary>
        /// ダウンロードしたファイルの保存先
        /// </summary>
        public string SaveFile { get; set; } = string.Empty;


        public void doTrim()
        {
            ApiKey = ApiKey.Trim();
            PleasanterURL = PleasanterURL.Trim();
            PleasanterURL = PleasanterURL.RemoveTrailingSlash();

            SaveFile = SaveFile.Trim();
        }

    }

    public static class StringExtensions
    {
        /// <summary>
        /// 文字列の末尾にあるスラッシュ（/）を削除します。
        /// </summary>
        /// <param name="input">処理する文字列</param>
        /// <returns>修正された文字列</returns>
        public static string RemoveTrailingSlash(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // 文字列の末尾がスラッシュであるか確認し、あれば削除
            return input.EndsWith("/") ? input.Remove(input.Length - 1) : input;
        }
    }

    public static class ConfigurationLogger
    {
        public static string ToStringForLogging(this RunConfiguration config)
        {
            var sb = new StringBuilder();

            sb.AppendLine("RunConfiguration:");
            sb.AppendLine($"- ApiKey: {config.ApiKey}");
            sb.AppendLine($"- PleasanterURL: {config.PleasanterURL}");
            sb.AppendLine($"- SiteID: {config.SiteID}");
            sb.AppendLine($"- SaveFile: {config.SaveFile}");

            return sb.ToString();
        }
    }


    public static class ConfigurationValidator
    {
        /// <summary>
        /// Configurationのプロパティが適切に設定されているか検証します。
        /// </summary>
        /// <param name="config">検証するConfigurationオブジェクト</param>
        public static string? Validate(this RunConfiguration config)
        {
            string? errorMessage = null;
            // ApiKeyのチェック
            if (string.IsNullOrWhiteSpace(config.ApiKey))
            {
                errorMessage = "APIキーは必須です。";
                return errorMessage;
            }

            // PleasanterURLのチェック
            if (string.IsNullOrWhiteSpace(config.PleasanterURL) || !Uri.IsWellFormedUriString(config.PleasanterURL, UriKind.Absolute))
            {
                errorMessage = "プリザンターのURLは有効なURLである必要があります。";
                return errorMessage;
            }

            // SiteIDのチェック
            if (config.SiteID < 0)
            {
                errorMessage = "サイトIDは0以上である必要があります。";
                return errorMessage;
            }

            return errorMessage;
        }
    }


    public static class PleasanterURLHelper
    {

        public static string ToGetsiteURL(this RunConfiguration configuration)
        {

            // POSTリクエストを送信するURL
            var url = $"{configuration.PleasanterURL}/api/items/{configuration.SiteID}/getsite";
            return url;
        }

    }




    public static class HttpClientHelper
    {

        public static async Task<HttpResponseMessage> GetPleasanterSiteInfo(this HttpClient client, RunConfiguration configuration)
        {

            // POSTリクエストを送信するURL
            //var url = "http://localhost:8081/api/items/2/getsite";

            // ApiRequestオブジェクトを作成し、プロパティを設定
            var requestObj = new ApiRequest
            {
                ApiKey = configuration.ApiKey
            };

            // オブジェクトをJSONにシリアライズ
            var jsonData = JsonSerializer.Serialize(requestObj);

            // StringContentを使用して、HTTPコンテンツを作成
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // POSTリクエストを非同期で送信し、レスポンスを待機
            var response = await client.PostAsync(configuration.ToGetsiteURL(), content);


            return response;
        }


    }
}


using System.Text.Json;
using GetPleasanterSite.Configuration;
using NLog;

namespace GetPleasanterSite
{
    public class GetSiteService
    {
        /// <summary>
        /// ロガー
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly RunConfiguration configuration;

        public GetSiteService(RunConfiguration ref_configuration)
        {
            this.configuration = ref_configuration;
        }

        public async Task doUpdate()
        {
            // HttpClientのインスタンスを作成
            using (var client = new HttpClient())
            {

                try
                {
                    logger.Info($"Pleasanter({this.configuration.PleasanterURL})から現在のスクリプト設定を読み込み開始");

                    // 現在の情報を取得
                    var response = await client.GetPleasanterSiteInfo(configuration);

                    // レスポンスが成功かどうかを確認
                    if (response.IsSuccessStatusCode)
                    {
                        // レスポンスボディを非同期で読み取り
                        var responseString = await response.Content.ReadAsStringAsync();


#if false
                        // JsonSerializerOptionsを設定して整形する
                        var options = new JsonSerializerOptions
                        {
                            WriteIndented = true
                        };

                        // JSONをパースし、整形して再シリアライズする
                        var jsonObject = JsonSerializer.Deserialize<JsonElement>(responseString);
                        string formattedJson = JsonSerializer.Serialize(jsonObject, options);
#endif

                        File.WriteAllText(configuration.SaveFile, responseString);
                    }
                    else
                    {
                        logger.Error("Error: " + response.StatusCode);
                    }
                }
                catch (Exception e)
                {
                    logger.Error("Exception caught: " + e.Message);
                }
            }


        }
    }
}


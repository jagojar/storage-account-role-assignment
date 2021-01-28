using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace StorageAccountApp
{
    public class AzureTokenManager
    {
        public ConfigBody _config { get; set; }

        public AzureTokenManager(ConfigBody config)
        {
            _config = config;
        }

        public async Task<TokenResponse> GetTokenAsync()
        {
            string url = string.Format("https://login.microsoftonline.com/{0}/oauth2/token", _config.tenantId);         

            var formCollection = new Dictionary<string, string>();
            formCollection.Add("grant_type", _config.grant_type);
            formCollection.Add("client_id", _config.client_id);
            formCollection.Add("client_secret", _config.client_secret);
            formCollection.Add("resource", _config.resource);

            var content = new FormUrlEncodedContent(formCollection);            

            var client = new HttpClient();
            var response = await client.PostAsync(url, content);

            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);

            return tokenResponse;
        }
    }
}
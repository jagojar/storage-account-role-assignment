using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace StorageAccountApp
{
    public class ResourceGroupManager
    {
        public ConfigBody _config { get; set; }
        public ResourceGroupManager(ConfigBody config)
        {
            _config = config;
        }
        public async Task<ResourceGroupResponse> GetResourceGroupsAsync(string token)
        {
            var subscriptionId = _config.subscriptionId;
            var urlRgs = string.Format("https://management.azure.com/subscriptions/{0}/resourcegroups?api-version=2020-06-01", subscriptionId);
                        
            var authorizationHeader = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = authorizationHeader;
            
            var response = await client.GetStringAsync(urlRgs);
            var rgResponse = JsonSerializer.Deserialize<ResourceGroupResponse>(response);

            return rgResponse;
        }
    }
}
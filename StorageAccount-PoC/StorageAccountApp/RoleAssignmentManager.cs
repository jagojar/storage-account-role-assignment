// https://docs.microsoft.com/en-us/azure/role-based-access-control/role-assignments-rest

//https://github.com/Azure/azure-libraries-for-net/blob/master/Samples/GraphRbac/ManageUsersGroupsAndRoles.cs

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StorageAccountApp
{
    public class RoleAssignmentManager
    {
        public ConfigBody _config { get; set; }

        public RoleAssignmentManager(ConfigBody config)
        {
            _config = config;
        }

        public async Task<RoleAssignmentResponse> SetAssignmentAsync(string authToken, RoleAssignmentParams raParams)
        {
            //Visual Studio Enterprise
            string subscriptionId = _config.subscriptionId;
            string roleAssignmentId = Guid.NewGuid().ToString();                        

            //Mary Smith
            string principalId = raParams.PrincipalId;
            string rgName = raParams.ResourceGroupName;
            string saName = raParams.StorageAccountName;
            string containerName = raParams.ContainerName;
                             
            string scope = $"subscriptions/{subscriptionId}/resourceGroups/{rgName}/providers/Microsoft.Storage/storageAccounts/{saName}/blobServices/default/containers/{containerName}";

            //Built-in-Role Storage Blob Data Reader
            //https://docs.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#storage-blob-data-reader
            string roleId = "2a2b9908-6ea1-4ae2-8e65-a410df84e7d1";
            string roleDefinitionId = $"{scope}/providers/Microsoft.Authorization/roleDefinitions/{roleId}";

            //PUT
            string assignmentUrl = $"https://management.azure.com/{scope}/providers/Microsoft.Authorization/roleAssignments/{roleAssignmentId}?api-version=2018-01-01-preview";

            var ra = new RoleAssigmentRequestBody()
            {
                properties = new RoleAssignmentProperty()
                {
                    roleDefinitionId = roleDefinitionId,
                    principalId = principalId
                }
            };            

            var client = new HttpClient();
            var authorizationHeader = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
            client.DefaultRequestHeaders.Authorization = authorizationHeader;            
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string raString = JsonSerializer.Serialize<RoleAssigmentRequestBody>(ra);
            var content = new StringContent(raString, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(assignmentUrl, content);
            var responseBody = response.Content.ReadAsStringAsync().Result;

            var raResponse = new RoleAssignmentResponse();
            raResponse.Message = "Error: See response body";            

            if (response.IsSuccessStatusCode)
            {                
                raResponse = JsonSerializer.Deserialize<RoleAssignmentResponse>(responseBody);
                raResponse.Message = "Successful Role assignment!!";
            }

            raResponse.ResponseBody = responseBody;

            return raResponse;
        }
    }
}
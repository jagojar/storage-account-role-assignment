using System;
using System.Threading.Tasks;

namespace StorageAccountApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Azure Role Assignment Storage Account!");
            Console.WriteLine("");
            Console.WriteLine("1. List of resource groups");
            Console.WriteLine("2. Role assignments");
            Console.Write("Enter option: ");

            var a = Console.ReadLine();

            var configManager = new ConfigManager();
            var configBody = configManager.GetConfig();
            
            var atManager = new AzureTokenManager(configBody);
            var response = atManager.GetTokenAsync().Result;
            //Console.WriteLine("token: " + response.access_token);

            switch(a)
            {
                case "1":
                    CallResourgeGroupsList(configBody, response.access_token);            
                    break;

                case "2":
                    CallRoleAssignmentStorageAccountContainer(configBody, response.access_token);
                    break;

                default:
                    Console.WriteLine("Option not available");
                break;
            }            
           
        }

        static void CallRoleAssignmentStorageAccountContainer(ConfigBody config, string token)
        {
            var raManager = new RoleAssignmentManager(config);

            Console.Write("PrincipalId (leave empty for '1f1f96f9 -cd7d-468e-8cfb-d241fbff99a2': ");
            string principalId = Console.ReadLine();

            if (string.IsNullOrEmpty(principalId))
                principalId = "1f1f96f9-cd7d-468e-8cfb-d241fbff99a2";

            Console.Write("Resource Group Name (leave empty for 'test-rg': ");
            string rgName = Console.ReadLine();

            if (string.IsNullOrEmpty(rgName))
                rgName = "test-rg";

            Console.Write("Storage Account Name (leave empty for 'sa36574457': ");
            string saName = Console.ReadLine();

            if (string.IsNullOrEmpty(saName))
                saName = "sa36574457";

            Console.Write("Container Name (leave empty for 'container1': ");
            string containerName = Console.ReadLine();

            if (string.IsNullOrEmpty(containerName))
                containerName = "container1";

            var raParams = new RoleAssignmentParams()
            {
                PrincipalId = principalId,
                ResourceGroupName = rgName,
                StorageAccountName = saName,
                ContainerName = containerName
            };

            var result = raManager.SetAssignmentAsync(token, raParams).Result;

            Console.WriteLine();
            Console.WriteLine(result.Message);
            Console.WriteLine();
            Console.WriteLine(result.ResponseBody);
        }

        static void CallResourgeGroupsList(ConfigBody config, string token)
        {
            var rgManager = new ResourceGroupManager(config);
            var groupsResponse = rgManager.GetResourceGroupsAsync(token).Result;
            
            Console.WriteLine("Resource Group list in subscription");
            Console.WriteLine("===================================");
            Console.WriteLine("NAME             LOCATION");
            Console.WriteLine("----             --------");

            foreach (ResourceGroupDetail rgd in groupsResponse.value)
            {
                Console.WriteLine($"{rgd.name}              {rgd.location}");
            }

        }
    }
}

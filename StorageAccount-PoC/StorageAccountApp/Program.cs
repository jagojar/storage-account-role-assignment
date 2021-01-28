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
            var result = raManager.SetAssignmentAsync(token).Result;            
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

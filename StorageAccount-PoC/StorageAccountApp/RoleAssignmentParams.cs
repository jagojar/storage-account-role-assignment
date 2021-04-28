using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAccountApp
{
    public class RoleAssignmentParams
    {
        public string PrincipalId { get; set; }
        public string ResourceGroupName { get; set; }
        public string StorageAccountName { get; set; }
        public string ContainerName { get; set; }
    }
}

using System.Collections.Generic;

namespace StorageAccountApp
{        
    public class RoleAssignmentResponse
    {
        public string id { get; set; }
        public string type { get; set; }
        public string name {get; set; }
        public RoleAssignmentDetail properties { get; set; }
    }

    public class RoleAssignmentDetail
    {
        public string roleDefinitionId { get; set; }
        public string principalId { get; set; }
        public string principalType { get; set; }
        public string scope { get; set; }
        public string createdOn { get; set; }
        public string updatedOn { get; set; }
        public string updatedBy { get; set; }
    }
}
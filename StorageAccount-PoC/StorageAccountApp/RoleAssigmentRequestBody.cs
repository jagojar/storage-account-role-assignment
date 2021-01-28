namespace StorageAccountApp
{
    public class RoleAssigmentRequestBody
    {
        public RoleAssignmentProperty properties { get; set; }
    }

    public class RoleAssignmentProperty
    {
        public string roleDefinitionId { get; set; }
        public string principalId { get; set; }
    }
}
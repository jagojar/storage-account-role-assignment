using System.Collections.Generic;

namespace StorageAccountApp
{
    public class ResourceGroupResponse
    {
       public IEnumerable<ResourceGroupDetail> value { get; set; }
    }

    public class ResourceGroupDetail
    {
        public string id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
    }
}
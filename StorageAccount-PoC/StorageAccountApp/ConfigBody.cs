namespace StorageAccountApp 
{
    public class ConfigBody
    {
        public string grant_type { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string resource { get; set; }
        public string tenantId { get; set; }
        public string subscriptionId { get; set; }        
    }
}
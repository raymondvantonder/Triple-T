namespace TripleT.User.Application.Common.Models.Configuration
{
    public class CosmosSettings
    {
        public string Endpoint { get; set; }
        public string PrimaryKey { get; set; }
        public string DatabaseName { get; set; }
        public string Containers { get; set; }
    }
}
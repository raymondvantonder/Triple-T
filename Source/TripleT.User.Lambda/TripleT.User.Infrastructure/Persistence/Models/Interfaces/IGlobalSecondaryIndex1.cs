using System.Text.Json.Serialization;

namespace TripleT.User.Infrastructure.Persistence.Models.Interfaces
{
    public interface IGlobalSecondaryIndex1
    {
        [JsonIgnore]
        static string GSI1IndexName  => "GSI1";
        string GSI1PK { get; set; }
        string GSI1SK { get; set; }
    }
}
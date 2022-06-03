using System.Text.Json.Serialization;

namespace TripleT.User.Infrastructure.Persistence.Models.Interfaces
{
    public interface IGlobalSecondaryIndex2
    {
        [JsonIgnore]
        static string GSI2IndexName  => "GSI2";
        string GSI2PK { get; set; }
        string GSI2SK { get; set; }
    }
}
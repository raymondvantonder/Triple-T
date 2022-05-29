using System.Text.Json;

namespace TripleT.User.Application.Common.Utilities
{
    public static class SerializerHelper
    {
        public static string SerializeObject<TValue>(TValue value)
            where TValue : class
        {
            return JsonSerializer.Serialize(value, new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        }
    }
}
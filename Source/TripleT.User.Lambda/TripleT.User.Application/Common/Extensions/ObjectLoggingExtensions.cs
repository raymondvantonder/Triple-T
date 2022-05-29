using TripleT.User.Application.Common.Utilities;

namespace TripleT.User.Application.Common.Extensions
{
    public static class ObjectLoggingExtensions
    {
        public static string FormatAsJsonForLogging(this object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return SerializerHelper.SerializeObject(value);
        }
    }
}

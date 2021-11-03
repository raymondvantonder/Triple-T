using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class SystemExtensions
    {
        public static int GetEnumCode(this Enum value)
        {
            return (int)Convert.ChangeType(value, value.GetTypeCode());
        }

    }
}

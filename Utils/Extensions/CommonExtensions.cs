namespace Utils.Extensions
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using ErrorHandling.Data;
    using Newtonsoft.Json;

    public static class CommonExtensions
    {
        public static string GetDescription(this Enum enumValue)
        {
            return enumValue
                .GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description ?? "";
        }

        public static string Stringify(this object obj, JsonSerializerSettings serializerSettings = null) 
            => JsonConvert.SerializeObject(obj, serializerSettings ?? new DefaultSerializerSettings());
    }
}

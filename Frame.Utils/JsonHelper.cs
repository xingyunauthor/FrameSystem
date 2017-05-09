using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Frame.Utils
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerSettings JsonSettings;

        static JsonHelper()
        {
            var datetimeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };

            JsonSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            JsonSettings.Converters.Add(datetimeConverter);
        }

        public static string ToJson<T>(this T t) where T : new()
        {
            return JsonConvert.SerializeObject(t, Formatting.None, JsonSettings);
        }

        public static T ToObject<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value, JsonSettings);
        }
    }
}

using Newtonsoft.Json;

namespace JsonHelper
{
    public static class Extensions
    {
        public static string ToJson(this object o, bool formatting = false)
            => JsonConvert.SerializeObject(o, formatting ? Formatting.Indented : Formatting.None);
        public static T FromJson<T>(this string json)
            => JsonConvert.DeserializeObject<T>(json);
        public static dynamic FromJson(this string json)
            => JsonConvert.DeserializeObject(json);
    }
}
using Newtonsoft.Json;

namespace NybSys.Common.Utility
{
    public static class JSONConvert
    {
        public static T Convert<T>(string jsonString) where T : class
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return JsonConvert.DeserializeObject<T>(jsonString, settings);
        }

        public static T Convert<T>(object obj) where T : class
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj), settings);
        }

        public static T Convert<T>(string jsonString, JsonSerializerSettings settings) where T : class
        {
            return JsonConvert.DeserializeObject<T>(jsonString, settings);
        }

        public static T Convert<T>(object obj, JsonSerializerSettings settings) where T : class
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj), settings);
        }

        public static string ConvertString(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}

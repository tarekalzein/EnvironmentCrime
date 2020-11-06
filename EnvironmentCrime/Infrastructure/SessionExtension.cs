using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EnvironmentCrime.Infrastructure
{
    public static class SessionExtension
    {
        /// <summary>
        /// Method to put (Value, key) pairs in serialized JSON.
        /// </summary>
        /// <param name="session">Linking the JSON serializer and deserializer to the current session</param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// Method to Deserialize JSON (Value, key) pairs.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null
                ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace DataAccesslayer.Services
{
    public static class OrderCart
    {
        // Set an object to session as JSON string
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // Get an object, from session, from JSON string
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}

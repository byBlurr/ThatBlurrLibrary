using Newtonsoft.Json;

namespace Blurr.Json
{
    /// <summary>
    /// Used for serializing and deserializing objects to and from Json
    /// </summary>
    public class Serialization
    {
        /// <summary>
        /// Prevent an instance of this class
        /// </summary>
        private Serialization() { }

        /// <summary>
        /// Convert an object to JSON
        /// </summary>
        /// <param name="obj">Object to convert</param>
        /// <returns>JSON string of obj</returns>
        public static string ToJson(object obj) => JsonConvert.SerializeObject(obj, Formatting.Indented);

        /// <summary>
        /// Convert string to Object
        /// </summary>
        /// <typeparam name="T">Type of object to convert JSON to</typeparam>
        /// <param name="json">JSON string to convert</param>
        /// <returns>Object of type T from JSON string</returns>
        public static T FromJson<T>(string json) => JsonConvert.DeserializeObject<T>(json);
    }
}

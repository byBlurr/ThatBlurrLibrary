using Newtonsoft.Json;
using System.IO;

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

        /// <summary>
        /// Save object to a json file
        /// </summary>
        /// <param name="path">Where to save the file to</param>
        /// <param name="obj">Object to save</param>
        public static void SaveToFile(string path, object obj) => File.WriteAllText(path, ToJson(obj));

        /// <summary>
        /// Load an object from a json file
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        /// <param name="path">The path that the json file is located at</param>
        /// <returns>Returns an object of type T from the json file</returns>
        public static T LoadFromFile<T>(string path) => FromJson<T>(File.ReadAllText(path));
    }
}

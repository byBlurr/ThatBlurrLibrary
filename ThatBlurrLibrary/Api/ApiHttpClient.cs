using Blurr.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Blurr.Api
{
    /// <summary>
    /// API Client using HTTP
    /// </summary>
    public class ApiHttpClient : ApiClient
    {
        private readonly HttpClient Client;
        private readonly JsonSerializerSettings Settings;

        public ApiHttpClient(string apiUrl, string userAgent = "Mozilla/5.0 (compatible; AcmeInc/1.0)") : base(apiUrl) 
        {
            Settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Add("User-Agent", userAgent);
        }

        /// <summary>
        /// Make an API call using HTTP
        /// </summary>
        /// <typeparam name="T">The type of object to return</typeparam>
        /// <param name="request">The request to make</param>
        /// <returns>Returns the object created by the JSON response</returns>
        public async Task<T> MakeCallAsync<T>(string request)
        {
            string response = await Client.GetStringAsync(Url + request);
            return Serialization.FromJson<T>(response, Settings);
        }
    }
}

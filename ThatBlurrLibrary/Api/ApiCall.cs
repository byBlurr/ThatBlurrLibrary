using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Blurr.Api
{
    public class ApiCall
    {

        /// <summary>
        /// The URL for the API calls
        /// </summary>
        private string Url = String.Empty;

        /// <summary>
        /// A collection of data used to authenticate calls...
        /// </summary>
        private readonly NameValueCollection AccessCollection;

        /// <summary>
        /// Initiate the ApiCall instance
        /// </summary>
        /// <param name="apiUrl">The url used to make API calls</param>
        /// <param name="accessCollection">The data required to access the api, for example username, password and accesskey. (Optional)</param>
        public ApiCall(string apiUrl, NameValueCollection accessCollection = null)
        {
            if (String.IsNullOrEmpty(apiUrl)) throw new Exception("No API url was provided for API Calls.");

            Url = apiUrl;
            AccessCollection = accessCollection;
        }

        /// <summary>
        /// Make an API call
        /// </summary>
        /// <param name="data">Data to pass through to api call</param>
        /// <returns>Returns the web response</returns>
        public string MakeCall(NameValueCollection data)
        {
            byte[] webResponse;
            NameValueCollection requestData = AccessCollection != null ? BuildRequestData(data) : data;

            try
            {
                webResponse = new WebClient().UploadValues(Url, requestData);
                return Encoding.ASCII.GetString(webResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Make an async API call
        /// </summary>
        /// <param name="data">Data to pass through to api call</param>
        /// <returns>Returns the web response</returns>
        public async Task<string> MakeCallAsync(NameValueCollection data)
        {
            byte[] webResponse;
            NameValueCollection requestData = AccessCollection != null ? BuildRequestData(data) : data;

            try
            {
                webResponse = await new WebClient().UploadValuesTaskAsync(Url, requestData);
                return Encoding.ASCII.GetString(webResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Add the access data to the data collection
        /// </summary>
        /// <param name="data">The data that needs the access data added to</param>
        /// <returns>Returns the new data collection</returns>
        private NameValueCollection BuildRequestData(NameValueCollection data)
        {
            NameValueCollection request = new NameValueCollection();

            if (AccessCollection != null)
            {
                foreach (string key in AccessCollection)
                {
                    request.Add(key, AccessCollection[key]);
                }
            }

            foreach (string key in data)
            {
                request.Add(key, data[key]);
            }

            return request;
        }
    }
}

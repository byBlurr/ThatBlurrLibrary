using System;
using System.Collections.Specialized;

namespace Blurr.Api
{
    /// <summary>
    /// Abstract class for API Client
    /// </summary>
    public abstract class ApiClient
    {
        /// <summary>
        /// The URL for the API calls
        /// </summary>
        protected string Url = String.Empty;


        /// <summary>
        /// A collection of data used to authenticate calls...
        /// </summary>
        protected readonly NameValueCollection AccessCollection = null;

        public ApiClient(string apiUrl, NameValueCollection accessCollection = null)
        {
            if (String.IsNullOrEmpty(apiUrl)) throw new Exception("No API url was provided for API Calls.");
            Url = apiUrl;
            AccessCollection = accessCollection;
        }
    }
}

using Blurr.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace UnitTest
{
    /// <summary>
    /// Tests for API methods
    /// </summary>
    [TestClass]
    public class ApiTests
    {
        /// <summary>
        /// Test making a call
        /// </summary>
        [TestMethod]
        public async Task HttpRequestTest()
        {
            ApiHttpClient client = new ApiHttpClient("https://api.github.com/");
            GitHubUser user = await client.MakeCallAsync<GitHubUser>("users/byBlurr");

            int expectedId = 20552533;
            Assert.AreEqual(expectedId, user.UserId);
            Assert.IsTrue(user.Hireable);
        }
    }

    public class GitHubUser
    {
        [JsonProperty("login")]
        public string Username { get; private set; }

        [JsonProperty("id")]
        public int UserId { get; private set; }

        [JsonProperty("node_id")]
        public string NodeId { get; private set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; private set; }

        [JsonProperty("gravatar_id")]
        public string GravatarId { get; private set; }

        [JsonProperty("name")]
        public string FullName { get; private set; }

        [JsonProperty("hireable")]
        public bool Hireable { get; private set; }
    }
}

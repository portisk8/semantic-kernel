using Microsoft.SemanticKernel;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace SK.Core.Plugins
{
    public class HeroInfo
    {
        static string apiKey;
        public HeroInfo(string superHeroApiKey)
        {
            apiKey = superHeroApiKey;
        }

        [KernelFunction, Description("Get info of a superhero")]
        public static async Task<string> GetAlterEgoAsync(string input)
        {
            // Call the API
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://superheroapi.com/api/{apiKey}/search/{input}");

            // Get the response
            var responseContent = await response.Content.ReadAsStringAsync();

            // Parse the response
            var json = JObject.Parse(responseContent);
            if (json["response"].ToString() == "error")
                return "";

            // Get the hero info
            var heroInfo = $"{json["results"][0]["biography"]}";

            // Return the hero info
            return heroInfo;
        }
    }
}

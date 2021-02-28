using System.Threading.Tasks;
using CumaBot.Function.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace CumaBot.Function.Functions
{
    public static class TriggerSa
    {
        [FunctionName("ImminoFunction")]
        public static async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request");

            string discordBotToken = DiscordUtils.GetDiscordBotToken();
            return new OkObjectResult(discordBotToken);
        }
    }
}
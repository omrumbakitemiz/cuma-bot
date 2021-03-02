using System.Threading.Tasks;
using CumaBot.Function.Utils;
using Discord;
using Discord.WebSocket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace CumaBot.Function.Functions
{
    public static class TriggerSa
    {
        private static DiscordSocketClient _client;
        private static ILogger _logger;

        [FunctionName("ImminoFunction")]
        public static async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            HttpRequest req, ILogger logger)
        {
            _logger = logger;

            _logger.LogInformation("C# HTTP trigger function processed a request");

            string token = DiscordUtils.GetDiscordBotToken();

            _client = new DiscordSocketClient();

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            _client.Ready += OnClientOnReady;
            
            return new OkResult();
        }

        private static Task OnClientOnReady()
        {
            DiscordUtils.SendGoodMorningMessage(_client, _logger);
            return Task.CompletedTask;
        }
    }
}
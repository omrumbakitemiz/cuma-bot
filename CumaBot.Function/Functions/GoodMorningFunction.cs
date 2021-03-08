using System;
using System.Threading.Tasks;
using CumaBot.Function.Utils;
using Discord;
using Discord.WebSocket;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace CumaBot.Function.Functions
{
    public static class GoodMorningFunction
    {
        private static DiscordSocketClient _client;
        private static string _token;

        [FunctionName("GoodMorning")]
        public static async Task SaAs([TimerTrigger("0 0 7 * * 1-5")] TimerInfo myTimer, ILogger logger)
        {
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");

            _client = new DiscordSocketClient();
            _token = DiscordUtils.GetDiscordBotToken();
            
            _client.Log += message => DiscordUtils.LogHandler(message, logger);

            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();

            _client.Ready += () => OnClientOnReady(logger);
        }

        private static Task OnClientOnReady(ILogger logger)
        {
            DiscordUtils.SendGoodMorningMessage(_client, logger);
            return Task.CompletedTask;
        }
    }
}
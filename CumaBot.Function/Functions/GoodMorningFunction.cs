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

        private static readonly string Token = DiscordUtils.GetDiscordBotToken();

        [FunctionName("GoodMorning")]
        public static async Task SaAs([TimerTrigger("0 0 0 * * 1-5")] TimerInfo myTimer, ILogger logger)
        {
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");

            _client = new DiscordSocketClient();

            _client.Log += message => DiscordUtils.LogHandler(message, logger);

            await _client.LoginAsync(TokenType.Bot, Token);
            await _client.StartAsync();

            _client.Ready += () => OnClientOnReady(logger);
        }

        private static async Task OnClientOnReady(ILogger logger)
        {
            SocketTextChannel socketTextChannel = DiscordUtils.GetTextChannel(_client, logger);
            Embed embed = new EmbedBuilder()
                .WithAuthor("immino")
                .WithTitle("Gunaydin")
                .WithDescription("GUNUNUZ AYDIN OLSUN MUBAREKLER")
                .WithThumbnailUrl("https://cdn.frankerfacez.com/emoticon/505217/4")
                .Build();
            
            await socketTextChannel.SendMessageAsync($"Gunaydin ey ahali", false, embed);
            logger.LogInformation("Sa As Message Sent!");
        }
    }
}
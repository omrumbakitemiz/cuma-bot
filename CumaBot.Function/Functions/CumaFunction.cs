using System;
using System.Threading.Tasks;
using CumaBot.Function.Utils;
using Discord;
using Discord.WebSocket;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace CumaBot.Function.Functions
{
    public static class Immino
    {
        private static readonly DiscordSocketClient DiscordClient = new DiscordSocketClient();
        private static readonly string Token = DiscordUtils.GetDiscordBotToken();

        [FunctionName("CallToCuma")]
        public static async Task RunAsync([TimerTrigger("0 0 8 * * Fri")] TimerInfo myTimer, ILogger logger)
        {
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");

            DiscordClient.Log += message => DiscordUtils.LogHandler(message, logger);
            DiscordClient.Ready += () => ClientOnReadyHandler(DiscordClient, logger);

            await DiscordClient.LoginAsync(TokenType.Bot, Token);
            await DiscordClient.StartAsync();
        }

        public static async Task ClientOnReadyHandler(DiscordSocketClient client, ILogger logger)
        {
            SocketTextChannel socketTextChannel = DiscordUtils.GetTextChannel(client, logger);
            await socketTextChannel.SendMessageAsync("https://www.youtube.com/watch?v=QDKMfyRcm7o");
            logger.LogInformation("Message Sent!");
        }
    }
}
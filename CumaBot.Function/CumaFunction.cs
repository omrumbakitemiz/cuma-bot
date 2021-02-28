using System;
using System.Threading.Tasks;
using CumaBot.Function.Utils;
using Discord;
using Discord.WebSocket;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CumaBot.Function
{
    public static class Immino
    {
        private static DiscordSocketClient _client;

        [FunctionName("GoodMorning")]
        public static async Task SaAs([TimerTrigger("0 0 */1 * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");

            _client = new DiscordSocketClient();

            _client.Log += DiscordUtils.HandleLog;

            IConfigurationRoot config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
            string token = config["DISCORD_BOT_TOKEN"];

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            _client.Ready += async () =>
            {
                SocketGuild socketGuild = _client.GetGuild(764931336191868939);
                SocketTextChannel socketTextChannel = socketGuild.GetTextChannel(804687744890699796);
                await socketTextChannel.SendMessageAsync($"Sa As # {DateTime.Now.Hour} - {DateTime.Now.Minute}");
                log.LogInformation("Sa As Message Sent!");
            };
        }

        [FunctionName("CallToCuma")]
        public static async Task RunAsync([TimerTrigger("0 0 8 * * Fri")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");

            _client = new DiscordSocketClient();

            _client.Log += DiscordUtils.HandleLog;

            var token = "ODEyMzE1OTM5NTA4OTc3NjY0.YC--LA.0TddcTf2EamPSBi-FmbjyVpybzQ";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            _client.Ready += () => DiscordUtils.ClientOnReady(log, _client);
        }
    }
}
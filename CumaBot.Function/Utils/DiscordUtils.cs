using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace CumaBot.Function.Utils
{
    public static class DiscordUtils
    {
        public static SocketTextChannel
        
        public static async Task ClientOnReady(ILogger logger, DiscordSocketClient client)
        {
            SocketGuild socketGuild = client.GetGuild(764931336191868939);
            SocketTextChannel socketTextChannel = socketGuild.GetTextChannel(804687744890699796);
            await socketTextChannel.SendMessageAsync("https://www.youtube.com/watch?v=QDKMfyRcm7o");
            logger.LogInformation("Message Sent!");
        }

        public static Task HandleLog(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
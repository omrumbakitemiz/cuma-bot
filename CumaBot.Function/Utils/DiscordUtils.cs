using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CumaBot.Function.Utils
{
    public static class DiscordUtils
    {
        public static IConfigurationRoot ConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
        }
        
        public static string GetDiscordBotToken()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
            return config["DISCORD_BOT_TOKEN"];
        }

        private static SocketGuild GetGuild(BaseSocketClient client, ILogger logger, ulong guildId = default)
        {
            logger.LogInformation($"Getting SocketGuild with guildId: {guildId}");

            IConfigurationRoot config = ConfigurationRoot();
            
            if (guildId == default)
            {
                guildId = Convert.ToUInt64(config["KS_TECH_SERVER_ID"]);
            }
            
            SocketGuild socketGuild = client.GetGuild(guildId);

            if (socketGuild == null)
            {
                logger.LogInformation($"SocketGuild not found with guildId: {guildId}");

                throw new ArgumentException($"guildId is wrong: {guildId}");
            }
            logger.LogInformation($"SocketGuild founded with guildId: {guildId}, SocketGuild: {socketGuild}");

            return socketGuild;
        }

        public static SocketTextChannel GetTextChannel(DiscordSocketClient client, ILogger logger, ulong channelId = default)
        {
            logger.LogInformation($"Getting SocketTextChannel with channelId: {channelId}");
            
            IConfigurationRoot config = ConfigurationRoot();
            
            if (channelId == default)
            {
                channelId = Convert.ToUInt64(config["GENERAL_CHANNEL_ID"]);
            }

            var guildId = Convert.ToUInt64(config["KS_TECH_SERVER_ID"]);
            logger.LogInformation($"GuildId: {guildId}");

            SocketGuild socketGuild = GetGuild(client, logger, guildId);
            logger.LogInformation($"Getting SocketTextChannel with channelId: {channelId}");

            SocketTextChannel socketTextChannel = socketGuild.GetTextChannel(channelId);
            
            if (socketTextChannel == null)
            {
                logger.LogInformation($"SocketTextChannel not found with channelId: {channelId}");

                throw new ArgumentException($"channelId is wrong {channelId}");
            }
            
            logger.LogInformation($"SocketTextChannel founded with channelId: {channelId}, SocketTextChannel: {socketTextChannel}");

            return socketTextChannel;
        }

        public static Task LogHandler(LogMessage message, ILogger logger)
        {
            logger.LogInformation(message.ToString());
            return Task.CompletedTask;
        }
        
        public static async void SendGoodMorningMessage(DiscordSocketClient client, ILogger logger)
        {
            SocketTextChannel socketTextChannel = GetTextChannel(client, logger);
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
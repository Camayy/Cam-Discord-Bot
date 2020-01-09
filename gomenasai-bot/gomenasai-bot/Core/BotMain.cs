using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Configuration;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace gomenasai_bot
{
    internal class Bot
    {
        //search image google drive https://www.daimto.com/search-files-on-google-drive-with-c/
        //make it so linked files can be uploaded with msg.content to attatch a word
        private static CommandService _commands;
        private static DiscordSocketClient _client;
        private static Core.EventHandler _handler;

        static void Main(string[] args)
        => new Bot().MainTask().GetAwaiter().GetResult();
        
        
        private async Task MainTask()
        {
            string connection = ConfigurationManager.ConnectionStrings["Token"].ConnectionString;
            _client = new DiscordSocketClient(new DiscordSocketConfig{LogLevel = LogSeverity.Debug});

            _commands = new CommandService(new CommandServiceConfig {CaseSensitiveCommands = true, DefaultRunMode = RunMode.Async, LogLevel = LogSeverity.Debug});
            await _client.LoginAsync(TokenType.Bot, connection);
                                                     
            await _client.StartAsync();
            await _client.SetGameAsync("staring at the wall", "http://www.google.com", StreamType.NotStreaming);
            _handler = new Core.EventHandler();
            await _handler.InitializeAsync(_client);
            await Task.Delay(-1);
        }

        public static CommandService GetCommands()
        {
            return _commands;
        }

        public static DiscordSocketClient GetClient()
        {
            return _client;
        }
    }
}

﻿using System;
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
        //TO DO:
        //SAVE EMOTE COUNTS     ----- USE JSON
        //SAVE REACTION COUNTS
        //USER COUNTS FOR EMOTES
        //MOST EMOTE USED           -------
        //SAVE MEMES AND UPLOAD
        //ADD KRIMS REPLACEMENT
        //READ BACK THROUGH MESSAGES - https://discordpy.readthedocs.io/en/latest/api.html#discord.Client.logs_from
        //other stuff
        public static CommandService _commands;
        public static DiscordSocketClient _client;
        public static Core.EventHandler _handler;

        static void Main(string[] args)
        => new Bot().MainTask().GetAwaiter().GetResult();
        
        
        private async Task MainTask()
        {
            string connection = ConfigurationManager.ConnectionStrings["Token"].ConnectionString;
            _client = new DiscordSocketClient(new DiscordSocketConfig{LogLevel = LogSeverity.Debug});

            _commands = new CommandService(new CommandServiceConfig {CaseSensitiveCommands = true, DefaultRunMode = RunMode.Async, LogLevel = LogSeverity.Debug});
            //NDk4OTMzNTI3MDkwMjk4OTIz.Dp08qA.Dp08qA.swNGS9YWYq_IdtcuyBQ5kryG3bM
            await _client.LoginAsync(TokenType.Bot, connection);
                                                     
            await _client.StartAsync();
            await _client.SetGameAsync("with daddys cummies", "http://www.google.com", StreamType.NotStreaming);
            //await _client.SetStatusAsync(UserStatus.DoNotDisturb);
            _handler = new Core.EventHandler();
            await _handler.InitializeAsync(_client);
            await Task.Delay(-1);
        }

        

        /* private Task _client_ReactionAdded(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
         {
             Console.WriteLine("_client_RactionAdded");
         }*/
    }
}
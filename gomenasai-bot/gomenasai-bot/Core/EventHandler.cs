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

namespace gomenasai_bot.Core
{
    internal class EventHandler
    {

        private DiscordSocketClient _client = Bot.GetClient();
        private CommandService _commands = Bot.GetCommands();

        public async Task InitializeAsync(DiscordSocketClient client)
        {
            
            _client = client;
            _commands = new CommandService();
            _client.MessageReceived += MessageSent;
            _client.UserJoined += UserJoined;
            _client.ReactionAdded += ReactionAdded;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());//move this to top?
            
            _client.Log += Utils.ConsoleLogging.ClientResponse;
        }

        private async Task ReactionAdded(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {
            Events.EmoteHandler.ReactionAdded(arg3);
            await Task.CompletedTask;
        }

        private async Task UserJoined(SocketGuildUser arg)
        {
            Events.EmoteHandler.NewUserJoined(arg);
            await Task.CompletedTask;
        }

        private async Task MessageSent(SocketMessage message)
        {
            int prefixPos = 0;

            var msg = message as SocketUserMessage;
            var context = new SocketCommandContext(_client, msg);
            
            
            //need to add await in the method to run asynchronously

            //await Commands.WaitWhatReaction.WaitWhatReactionForPerson(msg);
            //await Events.EmoteHandler.GetEmoteFromMessage(msg);
            Events.EmoteHandler.GetEmoteFromMessage(msg);
            //Events.EmoteUserHandler.GetEmoteFromMessage(msg); //remove this and class

            if (context.Message == null || context.Message.Content == "")
            {
                return;
            }
            
            if (context.User.IsBot)
            {
                return;
            }

            if (!(msg.HasStringPrefix("!", ref prefixPos) || msg.HasMentionPrefix(_client.CurrentUser, ref prefixPos)))
            {
                return;
            }

            var userMessage = await _commands.ExecuteAsync(context, prefixPos);

            if (!userMessage.IsSuccess)
            {
                Console.WriteLine(DateTime.Now + " Commands - Something went wrong with the commands: " + context.Message.Content + " Error reason: " + userMessage.ErrorReason);
            }

           
        }
    }
}

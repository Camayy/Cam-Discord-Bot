using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace gomenasai_bot
{
    class BotMain
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
        private CommandService _commands;
        private DiscordSocketClient _client;
        private int _niggaCount = 0;

        static void Main(string[] args)
          => new BotMain().MainTask().GetAwaiter().GetResult();
        

        private async Task MainTask()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            _commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug

            });

            _client.MessageReceived += MessageSent;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());

            
            _client.Ready += ClientReady;
            _client.Log += ClientResponse;
            


            await _client.LoginAsync(TokenType.Bot, "NDk4OTMzNTI3MDkwMjk4OTIz.Dp08qA.swNGS9YWYq_IdtcuyBQ5kryG3bM");
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task ClientResponse(LogMessage arg)   //private Func<LogMessage, Task> ClientOutput()
        {
            Console.WriteLine(DateTime.Now + " at " + arg.Source + " - " + arg.Message);
        }

        private async Task ClientReady()
        {
            await _client.SetGameAsync("with daddys cummies", "http://www.google.com", StreamType.NotStreaming);
        }

        private async Task MessageSent(SocketMessage message)
        {
            int prefixPos = 0;

            var msg = message as SocketUserMessage;
            var context = new SocketCommandContext(_client, msg);

            // await WaitWhat(msg);
            
            if (context.Message == null || context.Message.Content == "")
            {
                return;
            }
            var guild = context.Guild;
            IEmote emote = guild.Emotes.First(e => e.Name == "sheeeeeit");
            if (context.Message.Author.Id.Equals(98846349264457728))
            {
                await context.Message.AddReactionAsync(emote);//FIGURE OUT WHY THIS IS WEIRD DOESNT GO TO OTHER AWAITS
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
                Console.WriteLine(DateTime.Now+" Commands - Something went wrong with the commands: "+context.Message.Content+" Error reason: "+userMessage.ErrorReason);
            }

            await WaitWhatAli(msg);
            await GetEmoteFromMessage(msg);
            
            

        }

        private async Task GetEmoteFromMessage(SocketUserMessage msg)
        {
            var context = new SocketCommandContext(_client, msg);
            var guild = context.Guild;
            IEmote emote = guild.Emotes.First(e => e.Name == "waitwhat");
            
            if (Data.EmoteStorage.GetDictionaryCount() != guild.Emotes.Count)
            {
                //context.Message.Content ==
                //ADD THE NEW EMOTES -- do it with ID
                //Data.ReactionStorage.emoteCount.Add(new KeyValuePair<string, int>(EMOTENAME, 0));
                //ADD REACTION COUNT WITH_client.ReactionAdded += _client_ReactionAdded;
                Data.EmoteStorage.AddToDictionary("KEY OF THE NEW EMOTE", 1);
            }
            //GET THE EMOTE AND PASS IT IN HERE
            if (Data.EmoteStorage.ContainsKey("KEY"))
            {
                Data.EmoteStorage.UpdateDictionary("lol");

                
            }
        }
       /*private async Task WaitWhat(SocketUserMessage msg)
        {
            var context = new SocketCommandContext(_client, msg);

            var guild = context.Guild;
            IEmote emote = guild.Emotes.First(e => e.Name == "waitwhat");

            if (context.Message.Content == emote.ToString())//CHECKFOR REACTIONS AS WELL
            {
                _niggaCount = _niggaCount + 1;
                Console.WriteLine("WAITWHATCOUNT = "+ _niggaCount);
            }

        }*/

        private async Task WaitWhatAli(SocketUserMessage msg)
        {
            var context = new SocketCommandContext(_client, msg);
            var guild = context.Guild;
            IEmote emote = guild.Emotes.First(e => e.Name == "sheeeeeit");
            Console.WriteLine("USERS NAME= " + context.Message.Author.Username + "ID: " + context.Message.Author.Id);
            if (context.Message.Author.Id.Equals(98846349264457728))
            {
                await context.Message.AddReactionAsync(emote);
            }
        }

        /* private Task _client_ReactionAdded(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
         {
             Console.WriteLine("_client_RactionAdded");
         }*/
    }
}

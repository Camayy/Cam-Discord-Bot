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


namespace gomenasai_bot.Commands
{
    public class WaitWhatReaction : ModuleBase<SocketCommandContext>
    {
        private static readonly DiscordSocketClient _client = Bot.GetClient();

        public static async Task WaitWhatReactionForPerson(SocketUserMessage msg)
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
    }
}

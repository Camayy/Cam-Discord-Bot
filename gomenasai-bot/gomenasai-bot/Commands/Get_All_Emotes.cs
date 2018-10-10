using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace gomenasai_bot.Commands
{
    public class Get_All_Emotes : ModuleBase<SocketCommandContext>
    {
        [Command("GETALLMESSAGES"), Summary("Gets all the previous messages")]
        public async Task GetAllPreviousMessages()
        {
            var guild = Context.Guild;
            IEmote emote = guild.Emotes.First(e => e.Name == "waitwhat");
            await Context.Message.AddReactionAsync(emote);

            //var waitWhat = 
            //await Context.Message.AddReactionAsync("waitwhat");
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using Discord;
using Discord.Commands;

namespace gomenasai_bot.Commands
{
    public class GetNewEmotes : ModuleBase<SocketCommandContext>
    {
        [Command("addemote"), Summary("adds the emote")]
        public async Task CheckForNewEmote()
        {
            string emote = "";
            foreach (IEmote newEmote in Context.Guild.Emotes)
            {
                emote = newEmote.ToString();
                if(!Data.EmoteStorage.ContainsKey(emote) && !emote.Equals(null))
                {
                    Data.EmoteStorage.AddToDictionary(emote, 0);
                }
            }
            await Context.Channel.SendMessageAsync("Emotes up to date");
        }
    }
}

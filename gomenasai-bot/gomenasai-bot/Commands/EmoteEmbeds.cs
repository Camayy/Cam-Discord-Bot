using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace gomenasai_bot.Commands
{
    public class EmoteEmbeds : ModuleBase<SocketCommandContext>
    {
        [Command("emotelist"), Summary("Creates embed for emote list")]
        public async Task EmoteEmbed()
        {

            var dictionary = Data.EmoteStorage.GetDictionary().OrderByDescending(pair => pair.Value);
            //dictionary;

            EmbedBuilder embed = new EmbedBuilder();
            foreach(KeyValuePair<string,int> key in dictionary)
            {
                if (key.Value > 0)
                {
                    embed.AddField("Emote: " + key.Key, key.Value);
                }
            }
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}

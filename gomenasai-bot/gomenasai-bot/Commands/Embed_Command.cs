using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;


namespace gomenasai_bot.Commands
{

    public class Embed_Command : ModuleBase<SocketCommandContext>
    {
        [Command("embed"), Summary("Embed command")]
        public async Task Embed()
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor(Context.User.Username, Context.User.GetAvatarUrl());
            embed.WithColor(220, 20, 60);
            embed.WithImageUrl("http://i.imgur.com/oB2WbVy.jpg");
            embed.WithFooter("I HATE EM");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}

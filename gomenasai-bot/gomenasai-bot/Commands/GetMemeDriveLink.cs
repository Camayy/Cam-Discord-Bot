using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gomenasai_bot.Commands
{
    public class GetMemeDriveLink : ModuleBase<Discord.Commands.SocketCommandContext>
    {
        [Command("memes"), Summary("returns the link to the meme drive folder")]
        public async Task GetLink()
        {
            await Context.Channel.SendMessageAsync("https://drive.google.com/open?id=1FUmrpO1LYtYAWQPeyfVyvvbW4f7ON_p7");
        }
    }
}

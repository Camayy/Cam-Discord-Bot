using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using Discord;
using Discord.Commands;

namespace gomenasai_bot.Commands
{
    public class Faget_Command : ModuleBase<SocketCommandContext>
    {
        [Command("faggot"), Alias("fag", "faget", "nigger"), Summary("fuck off faget command")]
        public async Task HelloWorldCommand()
        {
            await Context.Channel.SendMessageAsync("ur a fucking faget");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using Discord;
using Discord.Commands;

namespace gomenasai_bot.Commands
{
    public class Chief_Command : ModuleBase<SocketCommandContext>
    {
        [Command("isthatit"), Alias("chief", "itaintit", "nigger"), Summary("That aint it chief")]
        public async Task ThatAintIt()
        {
            Random rand = new Random();
            int random = rand.Next(1, 3);

            string chief;

            if (random == 1)
            {
                chief = "After hours of deliberation with the Council of High Intelligence and Education Findings (C.H.I.E.F), it has been determined that the contents of this post will be categorized under the terminology \"This is it\" until further notice.";
            }
            else
            {
                chief = "After hours of deliberation with the Council of High Intelligence and Education Findings (C.H.I.E.F), it has been determined that the contents of this post will be categorized under the terminology \"This ain't it\" until further notice.";
            }
            await Context.Channel.SendMessageAsync(chief);
        }

    }
}

using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gomenasai_bot.Commands
{
    public class CommandList : ModuleBase<SocketCommandContext>
    {
        [Command("commands"), Summary("Gets all available commands")]
        public async Task GetCommmands()
        {
            Dictionary<string, string> commands = PopulateCommands();
            EmbedBuilder embed = new EmbedBuilder();
            embed.Color = Color.Red;
            foreach (KeyValuePair<string,string> cmd in commands)
            {
                embed.AddField("Command: "+cmd.Key, "Desc: "+cmd.Value);
            }
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        public Dictionary<string, string> PopulateCommands()
        {
            Dictionary<string, string> commands = new Dictionary<string, string>();
            commands.Add("!isthatit", "Gets a response from C.H.I.E.F");
            commands.Add("!emotelist", "Gets a list of all the emotes w/count");
            commands.Add("!memes", "Gets the memedrive");
            commands.Add("!addemote", "Adds a new emote");
            commands.Add("!useremote @user", "Gets the emotes for the user");
            commands.Add("!emotescore :emote:", "Gets leaderboard for emote");
            commands.Add("!artifact cardname", "Retrieves the card and stats");

            return commands;
        }
    }
    
}

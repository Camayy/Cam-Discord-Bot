using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gomenasai_bot.Commands
{
    public class SaveMemeCommand : ModuleBase<SocketCommandContext>
    {
        private static string[] _fileTypes = { "webm", "png", "jpeg", "jpg", "gif", "mp4" };

        [Command("savememe"), Summary("Saves recent images posted")]
        public async Task SaveRecentMemes()
        {
            var messages = await Context.Channel.GetMessagesAsync(5).Flatten();
            bool found = false;

            foreach (IUserMessage msg in messages)
            {
                if (msg.Attachments.Count > 0)
                {
                    Data.MemeDownloadUpload.HandleImages(msg, true, "");
                    found = true;
                    //break;
                }
                else
                {
                    foreach (string type in _fileTypes)
                    {
                        if (msg.Content.Contains(type))
                        {
                            Data.MemeDownloadUpload.HandleImages(msg, false, type);
                            found = true;
                        }
                    }
                }

            }
            if (found)
            {
                await Context.Channel.SendMessageAsync("Saved image");
            }
            else
            {
                await Context.Channel.SendMessageAsync("Could not find image");
            }
            

        }
}
}

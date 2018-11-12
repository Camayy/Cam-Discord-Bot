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

            foreach (IUserMessage msg in messages)
            {
                if (msg.Attachments.Count > 0)
                {
                    Data.MemeDownloadUpload.HandleImages(msg, true, "");
                    break;
                }
                else
                {
                    foreach (string type in _fileTypes)
                    {
                        if (msg.Content.Contains(type))
                        {
                            Data.MemeDownloadUpload.HandleImages(msg, false, type);
                        }
                    }
                }

            }
            

            await Context.Channel.SendMessageAsync("Saved image");

        }
}
}

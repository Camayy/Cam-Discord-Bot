/*using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gomenasai_bot.Events
{
    public static class EmoteUserHandler
    {
        private static readonly DiscordSocketClient _client = Bot._client;

        public static void GetEmoteFromMessage(SocketUserMessage msg)
        {
            Task.Run(() => ManipulateUserMessageForEmote(msg));
        }

        private static async Task ManipulateUserMessageForEmote(SocketUserMessage msg)
        {
            var context = new SocketCommandContext(_client, msg);
            var guild = context.Guild;
            foreach (IEmote emobe in guild.Emotes)
            {
                if (msg.Content.Contains(emobe.Name))
                {
                    if (Data.EmoteStorage.ContainsKey(emobe.ToString()))
                    {
                        Data.UserEmoteStorage.UpdateDictionary(msg, emobe.ToString());
                        break;
                    }
                }
            }
            await context.Channel.SendMessageAsync("");
        }
    }
}
*/
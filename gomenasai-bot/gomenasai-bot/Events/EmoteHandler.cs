using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Configuration;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace gomenasai_bot.Events
{
    class EmoteHandler
    {

        private static readonly DiscordSocketClient _client = Bot._client;

        public static async Task GetEmoteFromMessage(SocketUserMessage msg)
        {
            var context = new SocketCommandContext(_client, msg);
            var guild = context.Guild;
            IEmote emote = guild.Emotes.First(e => e.Name == "waitwhat");

            if (Data.EmoteStorage.GetDictionaryCount() != guild.Emotes.Count)
            {
                //context.Message.Content ==
                //ADD THE NEW EMOTES -- do it with ID
                //Data.ReactionStorage.emoteCount.Add(new KeyValuePair<string, int>(EMOTENAME, 0));
                //ADD REACTION COUNT WITH_client.ReactionAdded += _client_ReactionAdded;
                Data.EmoteStorage.AddToDictionary("KEY OF THE NEW EMOTE", 1);
            }
            //GET THE EMOTE AND PASS IT IN HERE
            if (Data.EmoteStorage.ContainsKey("KEY"))
            {
                Data.EmoteStorage.UpdateDictionary("lol");
            }
        }
    }
}

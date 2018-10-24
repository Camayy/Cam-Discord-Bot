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
    public static class EmoteHandler
    {

        private static readonly DiscordSocketClient _client = Bot._client;

        public static  void GetEmoteFromMessage(SocketUserMessage msg)
        {
            Task.Run(() => ManipulateUserMessage(msg));
        }

        private static async Task ManipulateUserMessage(SocketUserMessage msg)
        {
            var context = new SocketCommandContext(_client, msg);
            var guild = context.Guild;
            foreach (IEmote emobe in guild.Emotes)
            {
                if (msg.Content.Contains(emobe.Name))
                {
                    if (Data.EmoteStorage.ContainsKey(emobe.ToString()))
                    {
                        Data.EmoteStorage.UpdateDictionary(emobe.ToString());
                        Data.UserEmoteStorage.UpdateDictionary(msg, emobe.ToString());
                        break;     
                    }
                }
            }
            await context.Channel.SendMessageAsync("");
            //You'd be better off moving Emote stuff into another method and Task.Run it
        }

        //cant await the emote if message was not a emote
        // await context.Message.AddReactionAsync(emote); //THIS IS NOT AWAITING PROPERLY CHECK CMD FOR ERROR

        //handle if reaction already exists
        ///https://docs.stillu.cc/api/Discord.WebSocket.SocketReaction.html?q=socketreaction
        //handle none server emotes :smiling_imp:

        /*if (Data.EmoteStorage.GetDictionaryCount() != guild.Emotes.Count)
                    {
                        //context.Message.Content ==
                        //ADD THE NEW EMOTES -- do it with ID
                        //Data.ReactionStorage.emoteCount.Add(new KeyValuePair<string, int>(EMOTENAME, 0));
                        //ADD REACTION COUNT WITH_client.ReactionAdded += _client_ReactionAdded;
                        Data.EmoteStorage.AddToDictionary(emobe.ToString(), 1);
                    }*/

    }
}

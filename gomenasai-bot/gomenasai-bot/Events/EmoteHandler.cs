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
        public static SocketUserMessage _message;

        public static  void GetEmoteFromMessage(SocketUserMessage msg)
        {
            Task.Run(() => ManipulateUserMessage(msg));
        }

        public static void NewUserJoined(SocketGuildUser user)
        {
            Task.Run(() => AddNewUser(user));
        }

        private static async Task ManipulateUserMessage(SocketUserMessage msg)
        {
            _message = msg;
            var context = new SocketCommandContext(_client, msg);
            var guild = context.Guild;
            
            bool emote = UpdateGuildEmotes(msg, guild);

            if (emote != true)
            {
                UpdateEmojis(msg);
            }
            
            await context.Channel.SendMessageAsync("");
        }

        private static void UpdateEmojis(SocketUserMessage msg)
        {
            foreach (Data.WindowsEmoji emoj in Data.WindowsEmoji.Emojis.Values)
            {
                if (msg.Content == emoj._emoji)
                {
                    if (Data.EmoteStorage.ContainsKey(emoj._emoji))
                    {
                        UpdateDictionarys(emoj._emoji, msg);
                    }
                }
            }
        }

        private static bool UpdateGuildEmotes(SocketUserMessage msg, SocketGuild guild)
        {
            foreach (IEmote emobe in guild.Emotes)
            {
                if (msg.Content.Contains(emobe.Name))
                {
                    if (Data.EmoteStorage.ContainsKey(emobe.ToString()))
                    {
                        UpdateDictionarys(emobe.ToString(), msg);
                        return true;
                    }
                }
            }

            return false;
        }

        private static void UpdateDictionarys(string key, SocketUserMessage msg)
        {
            Data.EmoteStorage.UpdateDictionary(key);
            Data.UserEmoteStorage.UpdateDictionary(msg, key);
        }

        private static async Task AddNewUser(SocketGuildUser user)
        {
            Data.UserEmoteStorage.AddNewUser(user);
            await Task.CompletedTask;

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

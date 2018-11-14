using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;

using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Net;

namespace gomenasai_bot.Events
{
    public static class EmoteHandler
    {

        private static readonly DiscordSocketClient _client = Bot.GetClient();
        public static SocketUserMessage _msg;
        
        public static  void GetEmoteFromMessage(SocketUserMessage msg)
        {
            /*Thread t = new Thread(() => ManipulateUserMessage(msg));
            t.Start();
            t.oin();*/
            Task.Run(() => ManipulateUserMessage(msg));
        }

        public static void NewUserJoined(SocketGuildUser user)
        {
            Task.Run(() => AddNewUser(user));
        }

        public static void ReactionAdded(SocketReaction reac)
        {
            Task.Run(() => AddReaction(reac));
        }
        
        /// ///////////////////////////////////

        private static void AddReaction(SocketReaction reac)
        {
            //Data.UserEmoteStorage.UpdateDictionary(reac.User.ToString(), reac.Emote.ToString());
           
            foreach(Data.WindowsEmoji emoj in Data.WindowsEmoji.Emojis.Values)
            {
                if (emoj._emoji.Equals(reac.Emote.Name) || Data.UserEmoteStorage.ContainsKey(reac.Emote.ToString())) //|| Data.EmoteStorage.ContainsKey(emoj._emoji))
                {
                    UpdateDictionarys(reac.Emote.ToString(), reac.User.ToString());
                    break;
                }
            }
            
        }

        //private static async Task ManipulateUserMessage(SocketUserMessage msg)
        private static void ManipulateUserMessage(SocketUserMessage msg)
        {
            _msg = msg;
            var context = new SocketCommandContext(_client, msg);
            var guild = context.Guild;

            //if message contains emote from emotelist
                //update dictionary
            
            bool emote = UpdateGuildEmotes(msg, guild);

            if (!emote)
            {
                UpdateEmojis(msg);
            }

            // await context.Channel.SendMessageAsync("");
        }

        
        private static void UpdateEmojis(SocketUserMessage msg)
        {
            foreach (Data.WindowsEmoji emoj in Data.WindowsEmoji.Emojis.Values)
            {
                if (msg.Content.Contains(emoj._emoji))
                {
                    if (Data.EmoteStorage.ContainsKey(emoj._emoji))
                    {
                        UpdateDictionarys(emoj._emoji, msg.Author.ToString());
                    }
                }
            }
        }

        private static bool UpdateGuildEmotes(SocketUserMessage msg, SocketGuild _guild)
        {
            foreach (IEmote emobe in _guild.Emotes)
            _msg = msg;
            var context = new SocketCommandContext(_client, msg);
            var guild = context.Guild;

            try
            {
                foreach (IEmote emobe in guild.Emotes)
                {
                    if (msg.Content.Contains(emobe.Name))
                    {
                        UpdateDictionarys(emobe.ToString(), msg.Author.ToString());
                        return true;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR:" + e);
            }

            return false;
        }

        private static void UpdateDictionarys(string key, string author)
        {
            Data.EmoteStorage.UpdateDictionary(key);
            Data.UserEmoteStorage.UpdateDictionary(author, key);
        }

        private static async Task AddNewUser(SocketGuildUser user)
        {
            Data.UserEmoteStorage.AddNewUser(user);
            await Task.CompletedTask;

        }

    }
}

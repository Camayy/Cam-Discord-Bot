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
using System.Net;

namespace gomenasai_bot.Events
{
    public static class EmoteHandler
    {

        private static readonly DiscordSocketClient _client = Bot._client;
        public static SocketUserMessage _msg;

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
            _msg = msg;
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
                if (msg.Content.Contains(emoj._emoji))
                {
                    if (Data.EmoteStorage.ContainsKey(emoj._emoji))
                    {
                        UpdateDictionarys(emoj._emoji, msg);
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
                        UpdateDictionarys(emobe.ToString(), msg);
                        return true;
                    }
                }

                if (msg.Attachments.Count > 0)
                {
                    Data.MemeDownloadUpload.HandleImages(msg); //RUN AS SEPERATE THREAD
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR:" + e);
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

    }
}

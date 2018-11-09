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

        private static readonly DiscordSocketClient _client = Bot._client;
        public static SocketUserMessage _msg;

        private static string[] _fileTypes = { "webm", "png", "jpeg", "jpg", "gif", "mp4"};

        public static  void GetEmoteFromMessage(SocketUserMessage msg)
        {
            Thread t = new Thread(() => ManipulateUserMessage(msg));
            t.Start();
            t.Join();
        }

        public static void NewUserJoined(SocketGuildUser user)
        {
            Task.Run(() => AddNewUser(user));
        }

        //private static async Task ManipulateUserMessage(SocketUserMessage msg)
        private static void ManipulateUserMessage(SocketUserMessage msg)
        {
            _msg = msg;
            var context = new SocketCommandContext(_client, msg);
            var guild = context.Guild;
            
            bool emote = UpdateGuildEmotes(msg, guild);

            if (!emote)
            {
                UpdateEmojis(msg);
            }
            
            //this shit needs moving somewhere better

            foreach (string type in _fileTypes)
            {
                if (msg.Attachments.Count > 0)
                {
                    Data.MemeDownloadUpload.HandleImages(msg, true, "");
                    break;
                    
                }
                else if (msg.Content.Contains(type))
                {
                    Data.MemeDownloadUpload.HandleImages(msg, false, type);
                }
            }

            // await context.Channel.SendMessageAsync("");
            context.Channel.SendMessageAsync("");
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

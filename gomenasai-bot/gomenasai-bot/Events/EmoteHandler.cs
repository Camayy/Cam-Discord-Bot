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

            try
            {
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

                if (msg.Attachments.Count > 0)
                {
                    Data.MemeDownloadUpload.HandleImages(msg); //RUN AS SEPERATE THREAD
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR:" + e);
            }
            

            await context.Channel.SendMessageAsync("");
            //You'd be better off moving Emote stuff into another method and Task.Run it
        }

        private static async Task AddNewUser(SocketGuildUser user)
        {
            Data.UserEmoteStorage.AddNewUser(user);
            await Task.CompletedTask;

        }

    }
}

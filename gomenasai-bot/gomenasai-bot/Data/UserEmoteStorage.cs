using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace gomenasai_bot.Data
{
    [JsonObject]
    public class Users : IEnumerable<UserEmote>
    {
        public List<UserEmote> User { get; set; }

        public IEnumerator<UserEmote> GetEnumerator()
        {
            return User.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return User.GetEnumerator();
        }
    }
    public class UserEmote
    {
        public string UserId { get; set; }
        public Dictionary<string, int> UserEmoteDictionary { get; set; }
    }

    public class UserEmoteStorage
    {
        private static Dictionary<string, int> _emoteUserCount = null;
        private static Dictionary<string, int> _emotes = EmoteStorage.emoteCount;
        public static Users _serverUsers = null;
        private static UserEmote _user = null;
        private static readonly DiscordSocketClient _client = Bot._client;
        private static SocketUserMessage _msg = null;
        private static string _jsonString = "EmoteUserStorage.json";

        static UserEmoteStorage()
        {
            if (_serverUsers == null)
            {
                _serverUsers = DeserializeJson();
            }
        }
        
        public static void AddNewUser(SocketGuildUser user)//DO THIS ON USER JOIN SERVER
        {
            try
            {
                _user = new UserEmote();
                _user.UserId = user.ToString();
                _user.UserEmoteDictionary = GrabEmotes();

                _serverUsers.User.Add(_user);
                SaveData();
                Console.WriteLine("Added User: " + user.Username);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not add user: " + user.Username);
                Console.WriteLine("ERROR: " + e);
            }
            
        }

        /// <summary>
        /// creates the json file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static bool GetOrCreateJson(string file)
        {
            if (!File.Exists(file))
            {
                AddInitialUsers();
                return false;
            }
            return true;
        }

        /// <summary>
        /// grabs the emotes from other dictionary and makes value = 0 for all
        /// </summary>
        private static Dictionary<string,int> GrabEmotes()//make this return the dictionary
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (string key in _emotes.Keys)
            {
                dict.Add(key, 0);
            }
            return dict;
            //return _emoteUserCount;
        }

        /// <summary>
        /// populates the json file on creation
        /// </summary>
        private static void AddInitialUsers() 
        {
            _emoteUserCount = GrabEmotes();
            
            var context = new SocketCommandContext(_client, _msg);
            var guild = context.Guild;
            
            _serverUsers = new Users();

            List<UserEmote> users = new List<UserEmote>();
            
            foreach (Discord.WebSocket.SocketGuildUser user in guild.Users)
            {
                _user = new UserEmote();
                _user.UserId = user.ToString();
                _user.UserEmoteDictionary = _emoteUserCount;

                users.Add(_user);
            }

            _serverUsers.User = users;
            SaveData();
        }

        public static Users DeserializeJson()
        {
            Users list = null;
            try
            {
                list = JsonConvert.DeserializeObject<Users>(File.ReadAllText(_jsonString));
                //(Users<UserEmoteStorage)Newtonsoft.Json.JsonConvert.DeserializeObject(_jsonString), typeof(Users<UserEmoteStorage>));
                return list;
                
            }
            catch(Exception e)
            {
                Console.WriteLine("JSON ERROR: " + e);
                return list;
            }
        }

        public static void SaveData()
        {
            string json = JsonConvert.SerializeObject(_serverUsers, Formatting.Indented);
            File.WriteAllText(_jsonString, json);
        }

        public static void UpdateDictionary(SocketUserMessage message, string emote)//needs fixing
        {
            UserEmote user = new UserEmote();
            int i = 0;

           foreach(UserEmote usr in _serverUsers)//_serverusers ienumerable causing json not being able to read
            {
                if (usr.UserId  == message.Author.ToString())
                {
                    i = usr.UserEmoteDictionary[emote];
                    usr.UserEmoteDictionary[emote] = i + 1;
                    SaveData();
                    Console.WriteLine("Added emote " + emote + " to user: " + message.Author.ToString());
                    break;
                }
            }
        }
    }
    public class EmoteEmbeds : ModuleBase<SocketCommandContext>
    {

        [Command("useremote"), Summary("Creates embed for user emotes")]
        public async Task UserEmoteEmbed([Remainder]SocketUser userparam)
        {
            try
            {
                Users user = new Users();
                PropertyInfo[] properties = typeof(Users).GetProperties();
                int i = 0;
                EmbedBuilder embed = new EmbedBuilder();
                foreach (UserEmote usr in UserEmoteStorage._serverUsers)
                {
                    if (usr.UserId == userparam.ToString())
                    {
                        embed.AddField("User: ", usr.UserId);
                        var dictionary = usr.UserEmoteDictionary.OrderByDescending(pair => pair.Value);
                        foreach (KeyValuePair<string, int> key in dictionary)
                        {
                            embed.AddField("Emote: " + key.Key, "count: " + key.Value);
                        }
                        break;
                    }
                    //MAKE IT FOR INDIVIDUAL USER ONLY
                    // embed.AddField("User: "+usr.UserId, "Emotes: "+usr.UserEmoteDictionary.Keys[i],"Count: "+usr.UserEmoteDictionary.Values);/
                }

                //for each user
                //sort the emotes
                //add to embed

                //foreach user
                    //get each users emotes
                    //put them together
                    //compare them
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            catch(Exception e)
            {
                Console.WriteLine("error: " + e);
            }
        }
    }
}
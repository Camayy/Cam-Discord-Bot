using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gomenasai_bot.Data
{
    public class Users
    {
        public List<UserEmote> User { get; set; }
    }
    public class UserEmote
    {
        public string UserId { get; set; }
        public Dictionary<string, int> UserEmoteDictionary { get; set; }
    }

    public class UserEmoteStorage
    {
        private static Dictionary<string, int> _emoteUserCount = new Dictionary<string, int>();
        private static Dictionary<string, int> _emotes = EmoteStorage.emoteCount;
        private static Users _serverUsers = null;
        private static UserEmote _user = null;
        private static readonly DiscordSocketClient _client = Bot._client;
        private static SocketUserMessage _msg = null;
        private static string _jsonString = "EmoteUserStorage.json";

        static UserEmoteStorage()
        {
            //client = emotehandler.client
            //msg = emotehandler.msg
            if (_serverUsers == null)
            {
                DeserializeJson();
            }
        }

        public static bool AddToDictionary(string key, int value)//needs fixing
        {
            int count = GetDictionaryCount();
            _emoteUserCount.Add(key, value);
           // SaveData();
            if (GetDictionaryCount() > count)
            {
                return true;
            }
            return false;
        }

        public static int GetDictionaryCount()//needs fixing
        {
            return _emoteUserCount.Count;
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
        private static void GrabEmotes()//make this return the dictionary
        {
            foreach (string key in _emotes.Keys)
            {
                _emoteUserCount.Add(key, 0);
            }

            //return _emoteUserCount;
        }

        /// <summary>
        /// populates the json file on creation
        /// </summary>
        private static void AddInitialUsers() 
        {
            GrabEmotes();
            
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

            string json = JsonConvert.SerializeObject(_serverUsers, Formatting.Indented);
            File.WriteAllText(_jsonString, json);
        }

        public static Users DeserializeJson()
        {
            Users list = null;
            try
            {
                
                list = JsonConvert.DeserializeObject<Users>(File.ReadAllText(_jsonString));
                return list;
                
            }
            catch(Exception e)
            {
                Console.WriteLine("JSON ERROR: " + e);
                return list;
            }

            //var list = JsonConvert.DeserializeObject<List<UserEmote>>(_jsonString);
            //return list;

        }

        public static void SaveData(UserEmote user)//needs fixing
        {
            //string json = JsonConvert.SerializeObject(_emoteUserCount, Formatting.Indented);
            //File.WriteAllText(_jsonString, json);

            var list = DeserializeJson();
            //list.Add(user);
            var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
        }

        public static bool ContainsKey(string key)//needs fixing
        {
            if (_emoteUserCount.ContainsKey(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void UpdateDictionary(SocketUserMessage message, string key)//needs fixing
        {
            _msg = message;
            _serverUsers = new Users();

            var list = DeserializeJson();
           

            string json = File.ReadAllText(_jsonString);
            //_serverUsers = JsonConvert.DeserializeObject<Users>(json); //CONSTRUCTOR END

            

            //

            
            Discord.WebSocket.SocketUser ass = _msg.Author;
            //ADD TO THE USER HERE
            int i = _emoteUserCount[key];
            _emoteUserCount[key] = i + 1;
            //SaveData();
            Console.WriteLine("Added 1 count using:" + key);
        }
        
    }

    




    //THIS SHIT NEEDS TO BE MOVED SERVERUSERS.USER IS NULL
    /*public static void Dosomeshit()
    {
        var context = new SocketCommandContext(_client, _msg);//msg IS NULL PASS THROUGH THE CONSTRUCTOR SOMEHOW
        var guild = context.Guild;

        if (1 != guild.Users.Count)//if (serverUsers.User.Count != guild.Users.Count)
        //WHEN A USER JOINS SERVER
        //WHEN A USER LEAVES THE SERVER
        {
            List<UserEmote> list1 = _serverUsers.User;//serverusers not initialized
            IReadOnlyCollection<Discord.WebSocket.SocketGuildUser> list2 = guild.Users;

            var excludedIDs = new HashSet<string>(list1.Select(p => p.UserId));
            var result = list2.Where(p => !excludedIDs.Contains(p.Id.ToString()));
            Console.WriteLine("I hate my life");
            //serverUsers
        }

        if (_emoteUserCount.Count != EmoteStorage.GetDictionaryCount())
        {
            string[] keys = EmoteStorage.GetAllKeys();
            foreach (string key in _emoteUserCount.Keys)
            {
                foreach (string emoteStorageKey in keys)
                {
                    if (key != emoteStorageKey)
                    {
                        AddToDictionary(emoteStorageKey, 0);
                    }
                }
            }
            //foreach user
            //add each emote
        }
    }*/
}

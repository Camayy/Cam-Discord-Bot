using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Newtonsoft.Json;

namespace gomenasai_bot.Data
{
    public class EmoteStorage
    {
        public static Dictionary<string, int> emoteCount = new Dictionary<string, int>();

        /// <summary>
        /// creates/loads file data
        /// </summary>
        static EmoteStorage()
        {
            if (!GetOrCreateJson("EmoteStorage.json"))
            {
                return;
            }

            string json = File.ReadAllText("EmoteStorage.json");
            emoteCount = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
        }

        /// <summary>
        /// saves the data to the json file
        /// </summary>
        public static void SaveData()
        {
            string json = JsonConvert.SerializeObject(emoteCount, Formatting.Indented);
            File.WriteAllText("EmoteStorage.json", json);
        }

        /// <summary>
        /// Creates json file if it doesnt exists/Gets the file if it does
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static bool GetOrCreateJson(string file)
        {
            if (!File.Exists(file))
            {
                File.WriteAllText(file, "");
                SaveData();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the dictionary contains the key provided
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ContainsKey(string key)
        {
            if (emoteCount.ContainsKey(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// gets the amount of items in the dictionary
        /// </summary>
        /// <returns></returns>
        public static int GetDictionaryCount()
        {
            return emoteCount.Count;
        }

        /// <summary>
        /// Adds a item to the dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AddToDictionary(string key, int value)
        {
            int count = GetDictionaryCount();
            emoteCount.Add(key, value);
            SaveData();
            if (GetDictionaryCount() > count)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the value for the key provided
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetKeyCount(string key)
        {
            return emoteCount[key];
        }

        /// <summary>
        /// Updates the value in the dictionary for the key provided
        /// </summary>
        /// <param name="key"></param>
        public static void UpdateDictionary(string key)
        {
            int i = emoteCount[key];
            emoteCount[key] = i + 1;
            SaveData();
            Console.WriteLine("Added 1 count using:" + key);
        }

        public static string[] GetAllKeys()
        {
            string[] keys = null;
            int i = 0;
            foreach (string key in emoteCount.Keys)
            {
                keys[i] = key;
                i++;
            }
            return keys;
        }
    }
    
}

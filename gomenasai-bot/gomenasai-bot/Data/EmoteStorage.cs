using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Newtonsoft.Json;

namespace gomenasai_bot.Data
{
    class EmoteStorage
    {
        private static Dictionary<string, int> emoteCount = new Dictionary<string, int>();

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

        public static void SaveData()
        {
            string json = JsonConvert.SerializeObject(emoteCount, Formatting.Indented);
            File.WriteAllText("EmoteStorage.json", json);
        }

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

        public static int GetDictionaryCount()
        {
            return emoteCount.Count;
        }

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

        public static int GetKeyCount(string key)
        {
            return emoteCount[key];
        }

        public static void UpdateDictionary(string key)
        {
            int i = emoteCount[key];
            emoteCount[key] = i + 1;
        }
    }
}

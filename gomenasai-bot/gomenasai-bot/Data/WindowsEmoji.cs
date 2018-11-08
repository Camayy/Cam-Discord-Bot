using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gomenasai_bot.Data
{
    class WindowsEmoji
    {
        public string _name { get; set; }
        public string _emoji { get; set; }

        public static Dictionary<string, WindowsEmoji> Emojis = new Dictionary<string, WindowsEmoji>()
        {
           {"😏",
            new WindowsEmoji
            {
                _name = "smirk",
                _emoji = "😏"
            }
            },

            {"😤",
            new WindowsEmoji
            {
                _name = "triumph",
                _emoji = "😤"
            }
            },

            {"💪",
            new WindowsEmoji
            {
                _name = "muscle",
                _emoji ="💪"
            }
            },

            {"😂",
            new WindowsEmoji
            {
                _name = "joy",
                _emoji = "😂"
            }
            },

            {"😩",
            new WindowsEmoji
            {
                _name = "weary",
                _emoji = "😩"
            }
            },

            {"👀",
            new WindowsEmoji
            {
                _name = "eyes",
                _emoji = "👀"
            }
            },

            {"🤔",
            new WindowsEmoji
            {
                _name = "thinking",
                _emoji = "🤔"
            }
            },

            {"😌",
            new WindowsEmoji
            {
                _name = "relieved",
                _emoji = "😌"
            }
            },

            {"👌",
            new WindowsEmoji
            {
                _name = "ok_hand",
                _emoji = "👌"
            }
            },

            {"🙏",
            new WindowsEmoji
            {
                _name = "pray",
                _emoji = "🙏"
            }
            },

            {"😈",
            new WindowsEmoji
            {
                _name = "smiling_imp",
                _emoji = "😈"
            }
            },

            {"🤢",
            new WindowsEmoji
            {
                _name = "nauseated_face",
                _emoji = "🤢"
            }
            },

            {"😄",
            new WindowsEmoji
            {
                _name = "smile",
                _emoji = "😄"
            }
            },

            {"🤤",
            new WindowsEmoji
            {
                _name = "drooling_face",
                _emoji = "🤤"
            }
            },

            {"🍆",
            new WindowsEmoji
            {
                _name = "eggplant",
                _emoji = "🍆"
            }
            },

            {"💯",
            new WindowsEmoji
            {
                _name = "100",
                _emoji = "💯"
            }
            },

            {"😡",
            new WindowsEmoji
            {
                _name = "rage",
                _emoji = "😡"
            }
            },

            {"😠",
            new WindowsEmoji
            {
                _name = "angry",
                _emoji = "😠"
            }
            }
        };

        //void update
    
    }
}

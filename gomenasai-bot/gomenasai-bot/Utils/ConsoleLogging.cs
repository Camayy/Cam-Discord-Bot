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

namespace gomenasai_bot.Utils
{
    class ConsoleLogging
    {
        public static async Task ClientResponse(LogMessage arg)   //private Func<LogMessage, Task> ClientOutput()
        {
            Console.WriteLine(DateTime.Now +" at " + arg.Source + " - " + arg.Message);
            
        }
    }
}

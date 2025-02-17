using ChatApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.ExternalServices
{
    public class ChatGPTBotStrategy : IBotStrategy
    {
        public async Task<string> ProcessMessageAsync(string message)
        {
            await Task.Delay(100);
            return $"[ChatGPT] Processed message: {message}";
        }
    }
}

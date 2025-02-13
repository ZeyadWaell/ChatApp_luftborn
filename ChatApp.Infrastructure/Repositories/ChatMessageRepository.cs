using ChatApp.Core.Entities;
using ChatApp.Infrastructure.Data;
using ChatApp.Infrastructure.Repositories.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Repositories
{
    public class ChatMessageRepository : GenericRepository<ChatMessage>, IChatMessageRepository
    {
        public ChatMessageRepository(ChatDbContext context) : base(context)
        {
        }
    }
}

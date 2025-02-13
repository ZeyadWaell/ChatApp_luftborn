using ChatApp.Core.Entities.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Entities
{
    class ChatMessage : BaseEntity
    {
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public string Message { get; set; }

        public DateTime Timestamp { get; set; }

        public int ChatRoomId { get; set; }
        [ForeignKey(nameof(ChatRoomId))]
        public ChatRoom ChatRoom { get; set; }

        public bool IsBotMessage { get; set; }

        public ChatBotInteraction ChatBotInteraction { get; set; }

    }
}

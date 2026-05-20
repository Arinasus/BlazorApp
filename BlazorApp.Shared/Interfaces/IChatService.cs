using BlazorApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Interfaces
{
    public interface IChatService
    {
        Task<List<ChatMessage>> GetChatHistoryAsync(string user1Id, string user2Id);
        Task<bool> SendMessageAsync(ChatMessage message);
    }
}

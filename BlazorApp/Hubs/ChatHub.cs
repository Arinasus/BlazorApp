using Microsoft.AspNetCore.SignalR;

namespace BlazorApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToUser(string senderId, string senderName, string receiverId, string messageText)
        {
            // Пока временно шлем All (всем), чтобы обойти проблему с авторизацией сокетов.
            // Как только связь загорится зеленым, вернем точечную отправку!
            await Clients.All.SendAsync("ReceiveMessage", senderId, senderName, receiverId, messageText);
        }
    }
}

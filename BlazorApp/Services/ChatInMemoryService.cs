using BlazorApp.Components.Pages;

namespace BlazorApp.Services
{
        public class ChatInMemoryService
        {
            public event Action<string, string, string, string>? OnMessageReceived;

            private readonly List<StaticMessageDto> _history = new();

            public void BroadcastMessage(string senderId, string senderName, string receiverId, string text)
            {
                _history.Add(new StaticMessageDto
                {
                    SenderId = senderId,
                    SenderName = senderName,
                    Text = text
                });

                OnMessageReceived?.Invoke(senderId, senderName, receiverId, text);
            }

            public List<StaticMessageDto> GetHistory()
            {
                return _history;
            }

            public class StaticMessageDto
            {
                public string? SenderId { get; set; }
                public string? SenderName { get; set; }
                public string? Text { get; set; }
            }
        }
    }

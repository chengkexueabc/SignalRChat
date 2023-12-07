using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace SignalRChat
{
    public class MyHub : Hub
    {
        private readonly IMemoryCache memoryCache;

        public MyHub(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        public void CreateConnection(int userId)
        {
            // 将用户标识与 ConnectionId 关联起来
            memoryCache.Set(userId, Context.ConnectionId);
        }
        public async Task SendMessageToUser(int userId, string message)
        {
            if (memoryCache.TryGetValue(userId, out var connectionId))
            {
                await Clients.Client(connectionId.ToString()).SendAsync("ReceiveMessage", message);
            }
        }
    }
}

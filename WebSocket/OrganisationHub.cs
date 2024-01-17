using Core.WebSocketMessageKeys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebSocket;

[Authorize]
public class OrganisationHub : Hub {
    public async Task SendMessage(string user, string message) {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public override async Task OnConnectedAsync() {
        await Clients.All.SendAsync(OrgServerToClient.ClientJoined, "System",
            $"{Context.ConnectionId} joined the conversation");
        await base.OnConnectedAsync();
    }
}

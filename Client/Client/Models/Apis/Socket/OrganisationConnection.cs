using System;
using System.Threading.Tasks;
using Client.Models.Apis.Http;
using Common.WebSocketMessageKeys;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client.Models.Apis.Socket;

public class OrganisationConnection {
    private HubConnection? Connection { get; set; }

    public async Task Connect() {
        Connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7111/organisationHub", options => {
                    options.AccessTokenProvider = () => Task.FromResult(ApiClient.AccessToken)!;
                }
            )
            .Build();

        Connection.On<string, string>(OrgServerToClient.ClientJoined, (user, message) => {
            Console.WriteLine($"User: {user} Message: {message}");
        });

        await Connection.StartAsync();
    }

    public async Task SendMessage(string user, string message) {
        await Connection!.InvokeAsync(OrgClientToServer.SendMessage, user, message);
    }
}

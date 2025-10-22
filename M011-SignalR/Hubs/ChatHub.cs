using M000_DataAccess;
using Microsoft.AspNetCore.SignalR;

namespace M011_SignalR.Hubs;

public class ChatHub : Hub
{
	public ChatHub(KursDBContext db)
	{
		//Dependency Injection auch hier möglich
	}

	public override async Task OnConnectedAsync()
	{
		await Clients.All.SendAsync("UserVerbunden");
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		await Clients.All.SendAsync("UserGetrennt");
	}

	public async Task NachrichtSenden(string user, string msg)
	{
		await Clients.All.SendAsync("NachrichtEmpfangen", user, msg);
	}
}
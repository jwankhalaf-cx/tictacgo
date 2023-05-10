using Microsoft.AspNetCore.SignalR;
using TicTacGo.Enums;
using TicTacGo.Models;

namespace TicTacGo.Hubs
{
    public class GameHub : Hub
    {
        public async Task MakeMove(CellModel model)
        {
            await Clients.All.SendAsync("ReceiveMove", model);
        }
    }
}

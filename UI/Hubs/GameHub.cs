using Microsoft.AspNetCore.SignalR;
using TicTacGo.Enums;
using TicTacGo.Models;

namespace TicTacGo.Hubs
{
    public class GameHub : Hub
    {
        public async Task MakeMove(CellModel model)
        {
            Console.WriteLine($"You placed an {model.SetMark.ToString()} in cell with index: {model.CellIndex}");

            await Clients.All.SendAsync("ReceiveMove", model);
        }
    }
}

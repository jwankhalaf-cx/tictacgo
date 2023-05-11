using Microsoft.AspNetCore.SignalR;
using TicTacGo.Enums;
using TicTacGo.Models;

namespace TicTacGo.Hubs
{
    public class GameHub : Hub
    {
        public List<string> ConnectedPlayerIds = new List<string>();
        public Marks[] GameBoard = new Marks[] { Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet };

        public override async Task OnConnectedAsync()
        {
            ConnectedPlayerIds.Add(Context.ConnectionId);
            await Clients.All.SendAsync("DrawGameBoard", GameBoard);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            ConnectedPlayerIds.Remove(Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task MakeMove(PlayerMoveModel model)
        {
            GameBoard[model.CellIndex] = model.SetMark;

            await Clients.All.SendAsync("ReceiveMove", model);
        }
    }
}

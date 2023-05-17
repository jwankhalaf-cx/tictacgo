using Microsoft.AspNetCore.SignalR;
using UI.Enums;
using UI.Models;
using UI.Services.Interfaces;

namespace UI.Hubs;

public class GameHub : Hub
{
  private readonly IGameEngineService _gameEngineService;

  public GameHub(IGameEngineService gameEngineService)
  {
    _gameEngineService = gameEngineService;
  }

  public override async Task OnConnectedAsync()
  {
    Console.WriteLine($"OnConnectedAsync fired, Client Connection Id is: {Context.ConnectionId}");

    if (Context.GetHttpContext()?.GetRouteValue("GameCode") is string gameCode)
    {
      _gameEngineService.AddPlayerToGame(gameCode, Context.ConnectionId);

      if (!_gameEngineService.GameIsFull(gameCode))
      {
        await GiveTurn();

        Player playerOne = new()
        {
          ConnectionId = Context.ConnectionId,
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X
        };

        await JoinGame(playerOne);
      }
      else
      {
        Player playerTwo = new()
        {
          ConnectionId = Context.ConnectionId,
          Name = "Emma",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-PNG-Images-HD.png",
          Mark = Marks.O
        };

        await JoinGame(playerTwo);
      }

      await base.OnConnectedAsync();
    }
  }

  public override async Task OnDisconnectedAsync(Exception? exception)
  {
    Console.WriteLine($"OnDisconnectedAsync fired, Client Connection Id is: {Context.ConnectionId}");

    if (Context.GetHttpContext()?.GetRouteValue("GameCode") is string gameCode)
    {
      _gameEngineService.RemovePlayerFromGame(gameCode, Context.ConnectionId);

      await LeaveGame(new Player
        { ConnectionId = Context.ConnectionId, Name = "", ImageUrl = "", Mark = Marks.NotSet });

      await base.OnDisconnectedAsync(exception);
    }
  }

  public async Task GiveTurn()
  {
    await Clients.Others.SendAsync("GiveTurn");
  }

  private async Task JoinGame(Player model)
  {
    await Clients.All.SendAsync("AddPlayer", model);
  }

  private async Task LeaveGame(Player model)
  {
    await Clients.All.SendAsync("RemovePlayer", model);
  }

  public async Task MakeMove(MoveModel model)
  {
    await Clients.Others.SendAsync("ReceiveMove", model);
  }
}
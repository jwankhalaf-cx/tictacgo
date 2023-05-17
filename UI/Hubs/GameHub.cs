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
      bool gameExists = _gameEngineService.GameExists(gameCode);

      if (gameExists)
      {
        Player guest = new()
        {
          ConnectionId = Context.ConnectionId,
          Name = "Emma",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-PNG-Images-HD.png",
          Mark = Marks.O,
          HasTurn = false,
        };
        
        _gameEngineService.JoinGame(gameCode, guest);
      }
      else
      {
        Player host = new()
        {
          ConnectionId = Context.ConnectionId,
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true,
        };
        
        _gameEngineService.StartGame(gameCode, host);
      }

      Game? game = _gameEngineService.GetGame(gameCode);

      if (game is not null)
      {
        await Clients.All.SendAsync("RenderGame", game);
      }
      else
      {
        await Clients.All.SendAsync("ShowError", "game not found");
      }

      await base.OnConnectedAsync();
    }
  }

  public override async Task OnDisconnectedAsync(Exception? exception)
  {
    Console.WriteLine($"OnDisconnectedAsync fired, Client Connection Id is: {Context.ConnectionId}");

    if (Context.GetHttpContext()?.GetRouteValue("GameCode") is string gameCode)
    {
      await base.OnDisconnectedAsync(exception);
    }
  }

  public async Task MakeMove(string gameCode, Move model)
  {
    Game? game = _gameEngineService.MakeMove(gameCode, model);

    if (game is not null)
    {
      await Clients.All.SendAsync("RenderGame", game);
    }
  }
}
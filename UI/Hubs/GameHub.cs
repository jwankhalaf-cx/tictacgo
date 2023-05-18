using Microsoft.AspNetCore.SignalR;
using UI.Enums;
using UI.Mappers;
using UI.Models;
using UI.Services.Interfaces;

namespace UI.Hubs;

public class GameHub : Hub
{
  private readonly IGameEngineService _gameEngineService;
  private readonly IConverter<Entities.Game, Game> _gameMapper;

  public GameHub(
    IGameEngineService gameEngineService,
    IConverter<Entities.Game, Game> gameMapper)
  {
    _gameEngineService = gameEngineService;
    _gameMapper = gameMapper;
  }

  public override async Task OnConnectedAsync()
  {
    if (Context.GetHttpContext()?.GetRouteValue("GameCode") is string gameCode)
    {
      bool gameExists = _gameEngineService.GameExists(gameCode);

      if (gameExists)
      {
        Entities.Player guest = new()
        {
          ConnectionId = Context.ConnectionId,
          Name = "Emma",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-PNG-Images-HD.png",
          Mark = Marks.O,
          HasTurn = false
        };

        _gameEngineService.JoinGame(gameCode, guest);
      }
      else
      {
        Entities.Player host = new()
        {
          ConnectionId = Context.ConnectionId,
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true
        };

        _gameEngineService.StartGame(gameCode, host);
      }

      Entities.Game? game = _gameEngineService.GetGame(gameCode);

      if (game is not null)
      {
        Game gameDto = _gameMapper.Convert(game);

        await Clients.All.SendAsync("RenderGame", gameDto);
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
    if (Context.GetHttpContext()?.GetRouteValue("GameCode") is string gameCode)
    {
      _gameEngineService.LeaveGame(gameCode, Context.ConnectionId);

      await base.OnDisconnectedAsync(exception);
    }
  }

  public async Task MakeMove(string gameCode, Move model)
  {
    Entities.Game? game = _gameEngineService.MakeMove(gameCode, model);

    if (game is not null)
    {
      Game gameDto = _gameMapper.Convert(game);

      await Clients.All.SendAsync("RenderGame", gameDto);
    }
  }
}
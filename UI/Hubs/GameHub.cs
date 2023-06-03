using Microsoft.AspNetCore.SignalR;
using UI.Mappers;
using UI.Models;
using UI.Services.Interfaces;
using Game = UI.Entities.Game;

namespace UI.Hubs;

public class GameHub : Hub
{
  private readonly IGameEngine _gameEngine;
  private readonly IConverter<Game, Models.Game> _gameMapper;

  public GameHub(
    IGameEngine gameEngine,
    IConverter<Game, Models.Game> gameMapper)
  {
    _gameEngine = gameEngine;
    _gameMapper = gameMapper;
  }

  public override async Task OnConnectedAsync()
  {
    if (Context.GetHttpContext()?.GetRouteValue("GameCode") is string gameCode)
    {
      var gameExists = _gameEngine.GameExists(gameCode);

      if (gameExists)
      {
        await Groups.AddToGroupAsync(Context.ConnectionId, gameCode);

        _gameEngine.JoinGame(gameCode, Context.ConnectionId);
      }
      else
      {
        await Groups.AddToGroupAsync(Context.ConnectionId, gameCode);

        _gameEngine.StartGame(gameCode, Context.ConnectionId);
      }

      var game = _gameEngine.GetGame(gameCode);

      if (game is not null)
      {
        var gameDto = _gameMapper.Convert(game);

        await Clients.Group(gameCode).SendAsync("RenderGame", gameDto);
      }
      else
      {
        await Clients.Group(gameCode).SendAsync("ShowError", "game not found");
      }

      await base.OnConnectedAsync();
    }
  }

  public override async Task OnDisconnectedAsync(Exception? exception)
  {
    if (Context.GetHttpContext()?.GetRouteValue("GameCode") is string gameCode)
    {
      _gameEngine.LeaveGame(gameCode, Context.ConnectionId);
      await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameCode);
      await base.OnDisconnectedAsync(exception);
    }
  }

  public async Task MakeMove(string gameCode, Move model)
  {
    var game = _gameEngine.MakeMove(gameCode, model);

    if (game is not null)
    {
      // check if last move was a win or draw
      game.HasOutcome(model);

      var gameDto = _gameMapper.Convert(game);

      await Clients.Group(gameCode).SendAsync("RenderGame", gameDto);
    }
  }

  public async Task PlayNameChanged(string gameCode, string connectionId, string name)
  {
    var game = _gameEngine.SetPlayerName(gameCode, connectionId, name);

    if (game is not null)
    {
      var gameDto = _gameMapper.Convert(game);

      await Clients.Group(gameCode).SendAsync("RenderGame", gameDto);
    }
  }
}
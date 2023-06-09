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
      bool gameExists = _gameEngine.GameExists(gameCode);

      string[]? uriPath = Context.GetHttpContext()?.Request.Path.ToString().Split('/');

      string lastSegment = string.Empty;

      if (uriPath is { Length: > 3 }) lastSegment = uriPath[3];

      if (!string.IsNullOrEmpty(lastSegment) && lastSegment == "join" && !gameExists)
      {
        await Clients.Caller.SendAsync("NotAllowedToJoin");
        return;
      }

      bool? gameAlreadyHasTwoPlayers = _gameEngine.GetGame(gameCode)?.CanStart();

      if (gameAlreadyHasTwoPlayers is true)
      {
        await Clients.Caller.SendAsync("NotAllowedToJoin");
        return;
      }

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

      Game? game = _gameEngine.GetGame(gameCode);

      if (game is not null)
      {
        Models.Game gameDto = _gameMapper.Convert(game);

        await Clients.Group(gameCode).SendAsync("RenderGame", gameDto);
      }
      else
      {
        await Clients.Group(gameCode).SendAsync("ShowError", "game not found");
      }
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
    Game? game = _gameEngine.MakeMove(gameCode, model);

    if (game is not null)
    {
      // check if last move was a win or draw
      game.HasOutcome(model);

      Models.Game gameDto = _gameMapper.Convert(game);

      await Clients.Group(gameCode).SendAsync("RenderGame", gameDto);
    }
  }

  public async Task RestartGame(string gameCode)
  {
    Game? game = _gameEngine.ResetGame(gameCode);

    if (game is not null)
    {
      Models.Game gameDto = _gameMapper.Convert(game);

      await Clients.Group(gameCode).SendAsync("RenderGame", gameDto);
    }
  }

  public async Task PlayerNameChanged(string gameCode, string connectionId, string name)
  {
    Game? game = _gameEngine.SetPlayerName(gameCode, connectionId, name);

    if (game is not null)
    {
      Models.Game gameDto = _gameMapper.Convert(game);

      await Clients.Group(gameCode).SendAsync("RenderGame", gameDto);
    }
  }
}
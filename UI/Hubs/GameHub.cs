using Microsoft.AspNetCore.SignalR;
using UI.Enums;
using UI.Mappers;
using UI.Models;
using UI.Services.Interfaces;
using Game = UI.Entities.Game;
using Player = UI.Entities.Player;

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
      if (gameCode == "checkinggamecode") return;

      bool gameExists = _gameEngine.GameExists(gameCode);

      bool? gameAlreadyHasTwoPlayers = _gameEngine.GetGame(gameCode)?.CanStart();

      if (gameAlreadyHasTwoPlayers is true)
      {
        await Clients.Clients(Context.ConnectionId).SendAsync("NotAllowedToJoin", true);

        return;
      }

      if (gameExists)
      {
        Player guest = new()
        {
          ConnectionId = Context.ConnectionId,
          Name = "Emma",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-PNG-Images-HD.png",
          Mark = Marks.O,
          HasTurn = false
        };

        await Groups.AddToGroupAsync(Context.ConnectionId, gameCode);

        _gameEngine.JoinGame(gameCode, guest);
      }
      else
      {
        Player host = new()
        {
          ConnectionId = Context.ConnectionId,
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true
        };
        await Groups.AddToGroupAsync(Context.ConnectionId, gameCode);

        _gameEngine.StartGame(gameCode, host);
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

  public async void GameStatus(string gameCode)
  {
    Game? game = _gameEngine.GetGame(gameCode);

    if (game is not null && game.CanStart())
      await Clients.Clients(Context.ConnectionId).SendAsync("CheckGame", "NotAllowed");
    else if (game is null)
      await Clients.Clients(Context.ConnectionId).SendAsync("CheckGame", "WrongCode");
    else
      await Clients.Clients(Context.ConnectionId).SendAsync("CheckGame", "CanJoin");
  }
}
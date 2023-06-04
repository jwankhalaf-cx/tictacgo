using Microsoft.Extensions.Caching.Memory;
using UI.Models;
using UI.Services.Interfaces;
using Game = UI.Entities.Game;
using Player = UI.Entities.Player;

namespace UI.Services;

public class GameEngine : IGameEngine
{
  private readonly IMemoryCache _memoryCache;

  public GameEngine(IMemoryCache memoryCache)
  {
    _memoryCache = memoryCache;
  }

  public bool GameExists(string gameCode)
  {
    _memoryCache.TryGetValue(gameCode, out Game? game);

    return game is not null;
  }

  public void StartGame(string gameCode, Player host)
  {
    var game = new Game(gameCode, host);

    _memoryCache.Set(gameCode, game);
  }

  public void JoinGame(string gameCode, Player guest)
  {
    if (!GameExists(gameCode)) return;

    var game = GetGame(gameCode);

    if (game == null || game.CanStart()) return;

    game.AddGuest(guest);

    _memoryCache.Set(gameCode, game);
  }

  public void LeaveGame(string gameCode, string connectionId)
  {
    var game = GetGame(gameCode);

    game?.RemovePlayer(connectionId);

    _memoryCache.Set(gameCode, game);
  }

  public Game? GetGame(string gameCode)
  {
    _memoryCache.TryGetValue(gameCode, out Game? game);

    return game;
  }

  public Game? MakeMove(string gameCode, Move move)
  {
    var game = GetGame(gameCode);

    game?.Move(move);

    _memoryCache.Set(gameCode, game);

    return game;
  }

  public Game? ResetGame(string gameCode)
  {
    var game = GetGame(gameCode);
    game?.RestartGame();
    _memoryCache.Set(gameCode, game);
    return game;
  }
}
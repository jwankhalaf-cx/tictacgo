using Microsoft.Extensions.Caching.Memory;
using UI.Models;
using UI.Services.Interfaces;

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
    _memoryCache.TryGetValue(gameCode, out Entities.Game? game);

    return game is not null;
  }

  public void StartGame(string gameCode, Entities.Player host)
  {
    Entities.Game game = new Entities.Game(gameCode, host);

    _memoryCache.Set(gameCode, game);
  }

  public void JoinGame(string gameCode, Entities.Player guest)
  {
    if (!GameExists(gameCode)) return;

    Entities.Game? game = GetGame(gameCode);

    if (game == null || game.CanStart()) return;

    game.AddGuest(guest);

    _memoryCache.Set(gameCode, game);
  }

  public void LeaveGame(string gameCode, string connectionId)
  {
    Entities.Game? game = GetGame(gameCode);

    game?.RemovePlayer(connectionId);

    _memoryCache.Set(gameCode, game);
  }

  public Entities.Game? GetGame(string gameCode)
  {
    _memoryCache.TryGetValue(gameCode, out Entities.Game? game);

    return game;
  }

  public Entities.Game? MakeMove(string gameCode, Move move)
  {
    Entities.Game? game = GetGame(gameCode);

    game?.Move(move);

    _memoryCache.Set(gameCode, game);

    return game;
  }
}
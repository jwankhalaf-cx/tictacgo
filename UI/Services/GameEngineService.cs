using Microsoft.Extensions.Caching.Memory;
using UI.Models;
using UI.Services.Interfaces;

namespace UI.Services;

public class GameEngineService : IGameEngineService
{
  private readonly IMemoryCache _memoryCache;

  public GameEngineService(IMemoryCache memoryCache)
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
    Game game = new Game(gameCode, host);

    _memoryCache.Set(gameCode, game);
  }

  public void JoinGame(string gameCode, Player guest)
  {
    if (!GameExists(gameCode)) return;
    
    Game? game = GetGame(gameCode);

    if (game == null || game.CanStart()) return;
    
    game.AddGuest(guest);

    _memoryCache.Set(gameCode, game);
  }

  public Game? GetGame(string gameCode)
  {
    _memoryCache.TryGetValue(gameCode, out Game? game);

    return game;
  }

  public Game? MakeMove(string gameCode, Move move)
  {
    Game? game = GetGame(gameCode);

    game?.Move(move);

    _memoryCache.Set(gameCode, game);

    return game;
  }
}
using Microsoft.Extensions.Caching.Memory;
using UI.Enums;
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

  public void StartGame(string gameCode, string connectionId)
  {
    Player host = GenerateRandomPlayer(connectionId, Marks.X, true);

    Game game = new Game(gameCode, host);

    _memoryCache.Set(gameCode, game);
  }

  public void JoinGame(string gameCode, string connectionId)
  {
    if (!GameExists(gameCode)) return;

    Game? game = GetGame(gameCode);

    if (game == null || game.CanStart()) return;

    Player guest = GenerateRandomPlayer(connectionId, Marks.O, false);

    game.AddGuest(guest);

    _memoryCache.Set(gameCode, game);
  }

  public void LeaveGame(string gameCode, string connectionId)
  {
    Game? game = GetGame(gameCode);

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
    Game? game = GetGame(gameCode);

    game?.Move(move);

    _memoryCache.Set(gameCode, game);

    return game;
  }

  public Game? SetPlayerName(string gameCode, string connectionId, string name)
  {
    Game? game = GetGame(gameCode);

    game?.SetPlayerName(connectionId, name);

    _memoryCache.Set(gameCode, game);

    return game;
  }

  private static Player GenerateRandomPlayer(string connectionId, Marks mark, bool hasTurn)
  {
    string randomName = $"User{Guid.NewGuid().ToString("n")[..6]}";

    Player player = new()
    {
      ConnectionId = connectionId,
      Name = randomName,
      ImageUrl = $"https://ui-avatars.com/api/?name={randomName}&size=80&length=1&bold=true&format=svg",
      Mark = mark,
      HasTurn = hasTurn
    };

    return player;
  }

  public Game? ResetGame(string gameCode)
  {
    Game? game = GetGame(gameCode);

    game?.ResetGame();

    _memoryCache.Set(gameCode, game);

    return game;
  }
  public Game? NextRound(string gameCode)
  {
    Game? game = GetGame(gameCode);

    game?.NextRound();

    _memoryCache.Set(gameCode, game);

    return game;
  }
}
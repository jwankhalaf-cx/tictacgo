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
    Player host = GenerateRandomPlayer(connectionId);

    var game = new Game(gameCode, host);

    _memoryCache.Set(gameCode, game);
  }

  public void JoinGame(string gameCode, string connectionId)
  {
    if (!GameExists(gameCode)) return;

    var game = GetGame(gameCode);

    if (game == null || game.CanStart()) return;

    Player guest = new()
    {
      ConnectionId = connectionId,
      Name = "Emma",
      ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-PNG-Images-HD.png",
      Mark = Marks.O,
      HasTurn = false
    };

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

  public Game? SetPlayerName(string gameCode, string connectionId, string name)
  {
    var game = GetGame(gameCode);

    game?.SetPlayerName(connectionId, name);

    return game;
  }

  private static Player GenerateRandomPlayer(string connectionId)
  {
    string randomName = $"User{Guid.NewGuid().ToString("n")[..6]}";

    Player player = new()
    {
      ConnectionId = connectionId,
      Name = randomName,
      ImageUrl = $"https://ui-avatars.com/api/?name={randomName}&size=80&length=1&bold=true&format=svg",
      Mark = Marks.X,
      HasTurn = true
    };

    return player;
  }
}
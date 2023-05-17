using Microsoft.Extensions.Caching.Memory;
using UI.Services.Interfaces;

namespace UI.Services;

public class GameEngineService : IGameEngineService
{
  private readonly IDictionary<string, List<string>> _games = new Dictionary<string, List<string>>();
  private readonly IMemoryCache _memoryCache;

  public GameEngineService(IMemoryCache memoryCache)
  {
    _memoryCache = memoryCache;
  }

  public bool GameIsFull(string gameCode)
  {
    if (_games.TryGetValue(gameCode, out var players)) return players.Count == 2;

    return false;
  }

  public void AddPlayerToGame(string gameCode, string playerConnectionId)
  {
    if (_games.ContainsKey(gameCode))
    {
      var players = _games[gameCode];

      if (players.Count >= 2 || players.Contains(playerConnectionId)) return;

      players.Add(playerConnectionId);

      _games[gameCode] = players;
    }
    else
    {
      _games[gameCode] = new List<string> { playerConnectionId };
    }
  }

  public void RemovePlayerFromGame(string gameCode, string playerConnectionId)
  {
    if (!_games.ContainsKey(gameCode)) throw new Exception("cannot remove player his game doesn't exist!");

    var players = _games[gameCode];

    if (!players.Contains(playerConnectionId))
      throw new Exception("cannot remove player as player isn't present in the game!");

    players.Remove(playerConnectionId);

    _games[gameCode] = players;
  }
}
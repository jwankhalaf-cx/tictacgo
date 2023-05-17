using UI.Enums;

namespace UI.Models;

public class Game
{
  private readonly Marks[] _gameBoard =
  {
    Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet,
    Marks.NotSet
  };

  public Game(string id, Player host)
  {
    Id = id;
    Host = host;
  }

  public required string Id { get; set; }

  public required Player Host { get; set; }

  public Player? Guest { get; set; }

  public List<Player> GetPlayers()
  {
    return Guest != null ? new List<Player> { Host, Guest } : new List<Player> { Host };
  }
}
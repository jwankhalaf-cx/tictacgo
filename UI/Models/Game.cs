using System.Text;
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

  public string Id { get; set; }

  public Player? Host { get; set; }

  public Player? Guest { get; set; }

  public Marks[] GetBoard()
  {
    return _gameBoard;
  }

  public void AddGuest(Player guest)
  {
    Guest = guest;
  }

  public bool CanStart()
  {
    return Guest is not null;
  }

  public void Move(Move move)
  {
    _gameBoard[move.Index] = move.Mark;
  }

  public void RemovePlayer(string connectionId)
  {
    if (Host is not null)
      if (Host.ConnectionId == connectionId)
        Host = null;

    if (Guest is null) return;
    if (Guest.ConnectionId == connectionId)
      Guest = null;
  }

  public override string ToString()
  {
    var stringBuilder = new StringBuilder($"Game Code: {Id} | ");
    if (Host is not null) stringBuilder.Append($"Host Player: {Host.ToString()} || ");

    if (Guest is not null) stringBuilder.Append($"Guest Player: {Guest.ToString()} || ");

    foreach (var mark in _gameBoard) stringBuilder.Append($"{mark.ToString()} | ");

    return stringBuilder.ToString();
  }
}
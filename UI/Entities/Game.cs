using UI.Enums;
using UI.Models;

namespace UI.Entities;

public class Game
{
  private readonly Marks[] _gameBoard;

  public Game(string id, Player host)
  {
    Id = id;
    Host = host;
    _gameBoard = new[]
    {
      Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet,
      Marks.NotSet
    };
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
    if (Host != null && Host.ConnectionId == move.ConnectionId)
    {
      Host.HasTurn = false;
      if (Guest != null) Guest.HasTurn = true;
    }

    if (Guest != null && Guest.ConnectionId == move.ConnectionId)
    {
      Guest.HasTurn = false;
      if (Host != null) Host.HasTurn = true;
    }

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
}
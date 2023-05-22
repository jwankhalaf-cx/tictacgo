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
  public bool HasWon(Marks mark, Marks[] gameBoard)
    {
        if (HasWinningRow(mark, gameBoard) ||
            HasWinningColumn(mark, gameBoard) ||
            HasDiagonalWon(mark, gameBoard))
        {
            return true;
        }

        return false;
    }

    private bool HasWinningRow(Marks mark, Marks[] gameBoard)
    {
        if (gameBoard[0] == mark && gameBoard[1] == mark && gameBoard[2] == mark ||
            gameBoard[3] == mark && gameBoard[4] == mark && gameBoard[5] == mark ||
            gameBoard[6] == mark && gameBoard[7] == mark && gameBoard[8] == mark)
        {
            return true;
        }

        return false;
    }

    private bool HasWinningColumn(Marks mark, Marks[] gameBoard)
    {
        if (gameBoard[0] == mark && gameBoard[3] == mark && gameBoard[6] == mark ||
            gameBoard[1] == mark && gameBoard[4] == mark && gameBoard[7] == mark ||
            gameBoard[2] == mark && gameBoard[5] == mark && gameBoard[8] == mark)
        {
            return true;
        }

        return false;
    }

    private bool HasDiagonalWon(Marks mark, Marks[] gameBoard)
    {
        if (gameBoard[0] == mark && gameBoard[4] == mark && gameBoard[8] == mark ||
            gameBoard[2] == mark && gameBoard[4] == mark && gameBoard[6] == mark)
        {
            return true;
        }

        return false;
    }
}
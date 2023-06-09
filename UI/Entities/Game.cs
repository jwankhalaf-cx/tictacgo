using UI.Enums;
using UI.Models;

namespace UI.Entities;

public class Game
{
  private bool _hostPlayedFirst;
  private Marks[] _gameBoard;

  public Game(string id, Player host)
  {
    Id = id;
    Host = host;
    _hostPlayedFirst = true;
    _gameBoard = new[]
    {
      Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet,
      Marks.NotSet
    };
  }

  public string Id { get; }

  public Player? Host { get; private set; }

  public Player? Guest { get; private set; }

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
    return Host is not null && Guest is not null;
  }

  public void Move(Move move)
  {
    if (_gameBoard[move.Index] != Marks.NotSet) return;

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

  public GameOutcome? HasOutcome(Move model)
  {
    bool win = HasWinningRow(model.Mark) || HasWinningColumn(model.Mark) || HasDiagonalWon(model.Mark);

    switch (win)
    {
      case false:
        if (!CanContinue())
        {
          if (Host is not null && Guest is not null)
          {
            Host.HasTurn = false;
            Guest.HasTurn = false;
          }

          return GameOutcome.Draw;
        }
        else
        {
          return null;
        }
      case true:
        if (Host is not null && Host.ConnectionId == model.ConnectionId && Host.Mark == model.Mark)
        {
          Host.HasWon = true;
          Host.HasTurn = false;
          if (Guest is not null) Guest.HasTurn = false;
        }
        else if (Guest is not null && Guest.ConnectionId == model.ConnectionId && Guest.Mark == model.Mark)
        {
          Guest.HasWon = true;
          Guest.HasTurn = false;
          if (Host is not null) Host.HasTurn = false;
        }

        return GameOutcome.Win;
    }
  }

  private bool CanContinue()
  {
    return _gameBoard.Any(x => x == Marks.NotSet);
  }

  private bool HasWinningRow(Marks mark)
  {
    return (_gameBoard[0] == mark && _gameBoard[1] == mark && _gameBoard[2] == mark) ||
           (_gameBoard[3] == mark && _gameBoard[4] == mark && _gameBoard[5] == mark) ||
           (_gameBoard[6] == mark && _gameBoard[7] == mark && _gameBoard[8] == mark);
  }

  private bool HasWinningColumn(Marks mark)
  {
    return (_gameBoard[0] == mark && _gameBoard[3] == mark && _gameBoard[6] == mark) ||
           (_gameBoard[1] == mark && _gameBoard[4] == mark && _gameBoard[7] == mark) ||
           (_gameBoard[2] == mark && _gameBoard[5] == mark && _gameBoard[8] == mark);
  }

  private bool HasDiagonalWon(Marks mark)
  {
    return (_gameBoard[0] == mark && _gameBoard[4] == mark && _gameBoard[8] == mark) ||
           (_gameBoard[2] == mark && _gameBoard[4] == mark && _gameBoard[6] == mark);
  }

  public void ResetGame()
  {
    _gameBoard = new[]
    {
      Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet,
      Marks.NotSet
    };

    if (Host is null || Guest is null) return;

    Host.HasWon = false;
    Guest.HasWon = false;

    if (_hostPlayedFirst)
    {
      Host.HasTurn = false;
      Guest.HasTurn = true;
    }
    else
    {
      Host.HasTurn = true;
      Guest.HasTurn = false;
    }

    _hostPlayedFirst = !_hostPlayedFirst;
  }
}
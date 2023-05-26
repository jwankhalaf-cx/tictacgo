using FluentAssertions;
using UI.Enums;
using UI.Models;
using UI.Tests.Enums;
using Game = UI.Entities.Game;
using Player = UI.Entities.Player;

namespace UI.Tests;

public class GameTests
{
  private readonly Move _model = new()
  {
    Index = 0,
    Mark = Marks.X
  };


  #region checking rows

  [Fact]
  public void HasOutcome_WhenThreeMatchingMarksInFirstRow_ShouldReturnWinningOutcome()
  {
    // arrange
    Game game = SetupGame(MarkPositions.RowOne);

    //act
    var outcome = game.HasOutcome(_model);

    // assert
    outcome.Should().NotBeNull();
    outcome.Should().Be(GameOutcome.Win);
  }

  [Fact]
  public void HasOutcome_WhenThreeMatchingMarksInSecondRow_ShouldReturnWinningOutcome()
  {
    // arrange
    Game game = SetupGame(MarkPositions.RowTwo);

    //act
    var outcome = game.HasOutcome(_model);

    // assert
    outcome.Should().NotBeNull();
    outcome.Should().Be(GameOutcome.Win);
  }

  [Fact]
  public void HasOutcome_WhenThreeMatchingMarksInThirdRow_ShouldReturnWinningOutcome()
  {
    // arrange
    Game game = SetupGame(MarkPositions.RowThree);

    //act
    var outcome = game.HasOutcome(_model);

    // assert
    outcome.Should().NotBeNull();
    outcome.Should().Be(GameOutcome.Win);
  }

  #endregion

  #region checking columns

  [Fact]
  public void HasOutcome_WhenThreeMatchingMarksInFirstColumn_ShouldReturnWinningOutcome()
  {
    // arrange
    Game game = SetupGame(MarkPositions.ColumnOne);

    //act
    //act
    var outcome = game.HasOutcome(_model);

    // assert
    outcome.Should().NotBeNull();
    outcome.Should().Be(GameOutcome.Win);
  }

  [Fact]
  public void HasOutcome_WhenThreeMatchingMarksInSecondColumn_ShouldReturnWinningOutcome()
  {
    // arrange
    Game game = SetupGame(MarkPositions.ColumnTwo);

    //act
    var outcome = game.HasOutcome(_model);

    // assert
    outcome.Should().NotBeNull();
    outcome.Should().Be(GameOutcome.Win);
  }

  [Fact]
  public void HasOutcome_WhenThreeMatchingMarksInThirdColumn_ShouldReturnWinningOutcome()
  {
    // arrange
    Game game = SetupGame(MarkPositions.ColumnThree);

    //act
    var outcome = game.HasOutcome(_model);

    // assert
    outcome.Should().NotBeNull();
    outcome.Should().Be(GameOutcome.Win);
  }

  #endregion

  #region checking Diagonal

  [Fact]
  public void HasOutcome_WhenThreeMatchingMarksForwardDiagonally_ShouldReturnWinningOutcome()
  {
    // arrange
    Game game = SetupGame(MarkPositions.DiagonalForward);

    //act
    var outcome = game.HasOutcome(_model);

    // assert
    outcome.Should().NotBeNull();
    outcome.Should().Be(GameOutcome.Win);
  }

  [Fact]
  public void HasOutcome_WhenThreeMatchingMarksBackwardDiagonally_ShouldReturnWinningOutcome()
  {
    // arrange
    Game game = SetupGame(MarkPositions.DiagonalBackward);

    //act
    var outcome = game.HasOutcome(_model);

    // assert
    outcome.Should().NotBeNull();
    outcome.Should().Be(GameOutcome.Win);
  }

  #endregion

  #region checking draw

  [Fact]
  public void HasOutcome_WhenNoWinMatchingPatternsExist_ShouldReturnDrawOutcome()
  {
    // arrange
    Game game = SetupGame(MarkPositions.Draw);

    //act
    var outcome = game.HasOutcome(_model);

    // assert
    outcome.Should().NotBeNull();
    outcome.Should().Be(GameOutcome.Draw);
  }

  #endregion

  #region checking a null outcome

  [Fact]
  public void HasOutcome_WhenNoWinMatchingPatternsExistAndBoardIsNotFull_ShouldReturnNullOutcome()
  {
    // arrange
    Game game = SetupGame(MarkPositions.RandomWithBoardNotFull);

    //act
    var outcome = game.HasOutcome(_model);

    // assert
    outcome.Should().BeNull();
  }

  #endregion

  private static Game SetupGame(MarkPositions positions)
  {
    Player host = new()
    {
      ConnectionId = "player one connection hub id",
      Name = "Dan",
      ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
      Mark = Marks.X,
      HasTurn = true
    };

    Player guest = new()
    {
      ConnectionId = "player two connection hub id",
      Name = "Emma",
      ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-PNG-Images-HD.png",
      Mark = Marks.O,
      HasTurn = false
    };

    const string gameId = "g00";

    Game game = new Game(gameId, host);

    game.AddGuest(guest);

    List<Move> moves = GetMoves(positions);

    foreach (var move in moves) game.Move(move);

    return game;
  }

  private static List<Move> GetMoves(MarkPositions positions)
  {
    List<Move> moves;

    switch (positions)
    {
      case MarkPositions.ColumnOne:
      {
        var move = new Move
        {
          Index = 0,
          Mark = Marks.X
        };

        var move2 = new Move
        {
          Index = 3,
          Mark = Marks.X
        };

        var move3 = new Move
        {
          Index = 6,
          Mark = Marks.X
        };

        moves = new List<Move> { move, move2, move3 };
        break;
      }
      case MarkPositions.ColumnTwo:
      {
        var move = new Move
        {
          Index = 1,
          Mark = Marks.X
        };

        var move2 = new Move
        {
          Index = 4,
          Mark = Marks.X
        };

        var move3 = new Move
        {
          Index = 7,
          Mark = Marks.X
        };

        moves = new List<Move> { move, move2, move3 };
        break;
      }
      case MarkPositions.ColumnThree:
      {
        var move = new Move
        {
          Index = 2,
          Mark = Marks.X
        };

        var move2 = new Move
        {
          Index = 5,
          Mark = Marks.X
        };

        var move3 = new Move
        {
          Index = 8,
          Mark = Marks.X
        };

        moves = new List<Move> { move, move2, move3 };
        break;
      }
      case MarkPositions.RowOne:
      {
        var move = new Move
        {
          Index = 0,
          Mark = Marks.X
        };

        var move2 = new Move
        {
          Index = 1,
          Mark = Marks.X
        };

        var move3 = new Move
        {
          Index = 2,
          Mark = Marks.X
        };

        moves = new List<Move> { move, move2, move3 };
        break;
      }
      case MarkPositions.RowTwo:
      {
        var move = new Move
        {
          Index = 3,
          Mark = Marks.X
        };

        var move2 = new Move
        {
          Index = 4,
          Mark = Marks.X
        };

        var move3 = new Move
        {
          Index = 5,
          Mark = Marks.X
        };

        moves = new List<Move> { move, move2, move3 };
        break;
      }
      case MarkPositions.RowThree:
      {
        var move = new Move
        {
          Index = 6,
          Mark = Marks.X
        };

        var move2 = new Move
        {
          Index = 7,
          Mark = Marks.X
        };

        var move3 = new Move
        {
          Index = 8,
          Mark = Marks.X
        };

        moves = new List<Move> { move, move2, move3 };
        break;
      }
      case MarkPositions.DiagonalForward:
      {
        var move = new Move
        {
          Index = 2,
          Mark = Marks.X
        };

        var move2 = new Move
        {
          Index = 4,
          Mark = Marks.X
        };

        var move3 = new Move
        {
          Index = 6,
          Mark = Marks.X
        };

        moves = new List<Move> { move, move2, move3 };
        break;
      }
      case MarkPositions.DiagonalBackward:
      {
        var move = new Move
        {
          Index = 0,
          Mark = Marks.X
        };

        var move2 = new Move
        {
          Index = 4,
          Mark = Marks.X
        };

        var move3 = new Move
        {
          Index = 8,
          Mark = Marks.X
        };

        moves = new List<Move> { move, move2, move3 };
        break;
      }
      case MarkPositions.Draw:
      {
        var move = new Move
        {
          Index = 0,
          Mark = Marks.X
        };

        var move2 = new Move
        {
          Index = 1,
          Mark = Marks.O
        };

        var move3 = new Move
        {
          Index = 2,
          Mark = Marks.X
        };

        var move4 = new Move
        {
          Index = 3,
          Mark = Marks.X
        };

        var move5 = new Move
        {
          Index = 4,
          Mark = Marks.X
        };

        var move6 = new Move
        {
          Index = 5,
          Mark = Marks.O
        };

        var move7 = new Move
        {
          Index = 6,
          Mark = Marks.O
        };

        var move8 = new Move
        {
          Index = 7,
          Mark = Marks.X
        };

        var move9 = new Move
        {
          Index = 8,
          Mark = Marks.O
        };

        moves = new List<Move> { move, move2, move3, move4, move5, move6, move7, move8, move9 };
        break;
      }
      case MarkPositions.RandomWithBoardNotFull:
      {
        var move = new Move
        {
          Index = 0,
          Mark = Marks.X
        };

        var move2 = new Move
        {
          Index = 1,
          Mark = Marks.O
        };

        var move3 = new Move
        {
          Index = 2,
          Mark = Marks.X
        };

        var move4 = new Move
        {
          Index = 3,
          Mark = Marks.X
        };

        moves = new List<Move> { move, move2, move3, move4 };
        break;
      }
      default:
        throw new ArgumentOutOfRangeException(nameof(positions), positions, null);
    }

    return moves;
  }
}
using UI.Enums;
using UI.Entities;
using FluentAssertions;

namespace UI.Tests;

public class GameTests
{
  private const Marks Mark = Marks.X;

  #region checking rows

  [Fact]
  public void HasOutcome_WhenThreeMatchingMarksInFirstRow_ShouldReturnWinningOutcome()
  {
    // arrange
    Game game = SetupGame(MarkPostions.RowOne);

    //act
    GameOutcome? outcome = game.HasOutcome(Mark);

    // assert
    outcome.Should().NotBeNull();
    outcome.Should().Be(GameOutcome.Win);
  }

  [Fact]
  public void HasOutcome_WhenThreeMatchingMarksInSecondRow_ShouldReturnWinningOutcome()
  {
    // arrange
    Game game = SetupGame(MarkPostions.RowTwo);

    //act
    GameOutcome? outcome = game.HasOutcome(Mark);

    // assert
    outcome.Should().NotBeNull();
    outcome.Should().Be(GameOutcome.Win);
  }

  [Fact]
  public void HasOutcome_WhenThreeMatchingMarksInThirdRow_ShouldReturnWinningOutcome()
  {
    // arrange
    Game game = SetupGame(MarkPostions.RowThree);

    //act
    GameOutcome? outcome = game.HasOutcome(Mark);

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
    Game game = SetupGame(MarkPostions.ColumnOne);

    //act
    //act
    GameOutcome? outcome = game.HasOutcome(Mark);

    // assert
    outcome.Should().NotBeNull();
    outcome.Should().Be(GameOutcome.Win);
  }

  [Fact]
  public void HasOutcome_WhenThreeMatchingMarksInSecondColumn_ShouldReturnWinningOutcome()
  {
    // arrange
    Game game = SetupGame(MarkPostions.ColumnTwo);

    //act
    GameOutcome? outcome = game.HasOutcome(Mark);

    // assert
    outcome.Should().NotBeNull();
    outcome.Should().Be(GameOutcome.Win);
  }

  [Fact]
  public void HasOutcome_WhenThreeMatchingMarksInThirdColumn_ShouldReturnWinningOutcome()
  {
    // arrange
    Game game = SetupGame(MarkPostions.ColumnThree);

    //act
    GameOutcome? outcome = game.HasOutcome(Mark);

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
    Game game = SetupGame(MarkPostions.DiagonalForward);

    //act
    GameOutcome? outcome = game.HasOutcome(Mark);

    // assert
    outcome.Should().NotBeNull();
    outcome.Should().Be(GameOutcome.Win);
  }

  [Fact]
  public void HasOutcome_WhenThreeMatchingMarksBackwardDiagonally_ShouldReturnWinningOutcome()
  {
    // arrange
    Game game = SetupGame(MarkPostions.DiagonalBackward);

    //act
    GameOutcome? outcome = game.HasOutcome(Mark);

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
    Game game = SetupGame(MarkPostions.Draw);

    //act
    GameOutcome? outcome = game.HasOutcome(Mark);

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
    Game game = SetupGame(MarkPostions.RandomWithBoardNotFull);

    //act
    GameOutcome? outcome = game.HasOutcome(Mark);

    // assert
    outcome.Should().BeNull();
  }

  #endregion

  private static Game SetupGame(MarkPostions positions)
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

    List<Models.Move> moves = GetMoves(positions);

    foreach (var move in moves) game.Move(move);

    return game;
  }

  private static List<Models.Move> GetMoves(MarkPostions positions)
  {
    List<Models.Move> moves;

    switch (positions)
    {
      case MarkPostions.ColumnOne:
      {
        var move = new Models.Move
        {
          Index = 0,
          Mark = Marks.X
        };
        var move2 = new Models.Move
        {
          Index = 3,
          Mark = Marks.X
        };
        var move3 = new Models.Move
        {
          Index = 6,
          Mark = Marks.X
        };
        moves = new List<Models.Move> { move, move2, move3 };
        break;
      }
      case MarkPostions.ColumnTwo:
      {
        var move = new Models.Move
        {
          Index = 1,
          Mark = Marks.X
        };
        var move2 = new Models.Move
        {
          Index = 4,
          Mark = Marks.X
        };
        var move3 = new Models.Move
        {
          Index = 7,
          Mark = Marks.X
        };
        moves = new List<Models.Move> { move, move2, move3 };
        break;
      }
      case MarkPostions.ColumnThree:
      {
        var move = new Models.Move
        {
          Index = 2,
          Mark = Marks.X
        };
        var move2 = new Models.Move
        {
          Index = 5,
          Mark = Marks.X
        };
        var move3 = new Models.Move
        {
          Index = 8,
          Mark = Marks.X
        };
        moves = new List<Models.Move> { move, move2, move3 };
        break;
      }
      case MarkPostions.RowOne:
      {
        var move = new Models.Move
        {
          Index = 0,
          Mark = Marks.X
        };
        var move2 = new Models.Move
        {
          Index = 1,
          Mark = Marks.X
        };
        var move3 = new Models.Move
        {
          Index = 2,
          Mark = Marks.X
        };
        moves = new List<Models.Move> { move, move2, move3 };
        break;
      }
      case MarkPostions.RowTwo:
      {
        var move = new Models.Move
        {
          Index = 3,
          Mark = Marks.X
        };
        var move2 = new Models.Move
        {
          Index = 4,
          Mark = Marks.X
        };
        var move3 = new Models.Move
        {
          Index = 5,
          Mark = Marks.X
        };
        moves = new List<Models.Move> { move, move2, move3 };
        break;
      }
      case MarkPostions.RowThree:
      {
        var move = new Models.Move
        {
          Index = 6,
          Mark = Marks.X
        };
        var move2 = new Models.Move
        {
          Index = 7,
          Mark = Marks.X
        };
        var move3 = new Models.Move
        {
          Index = 8,
          Mark = Marks.X
        };
        moves = new List<Models.Move> { move, move2, move3 };
        break;
      }
      case MarkPostions.DiagonalForward:
      {
        var move = new Models.Move
        {
          Index = 2,
          Mark = Marks.X
        };
        var move2 = new Models.Move
        {
          Index = 4,
          Mark = Marks.X
        };
        var move3 = new Models.Move
        {
          Index = 6,
          Mark = Marks.X
        };
        moves = new List<Models.Move> { move, move2, move3 };
        break;
      }
      case MarkPostions.DiagonalBackward:
      {
        var move = new Models.Move
        {
          Index = 0,
          Mark = Marks.X
        };
        var move2 = new Models.Move
        {
          Index = 4,
          Mark = Marks.X
        };
        var move3 = new Models.Move
        {
          Index = 8,
          Mark = Marks.X
        };
        moves = new List<Models.Move> { move, move2, move3 };
        break;
      }
      case MarkPostions.Draw:
      {
        var move = new Models.Move
        {
          Index = 0,
          Mark = Marks.X
        };
        var move2 = new Models.Move
        {
          Index = 1,
          Mark = Marks.O
        };
        var move3 = new Models.Move
        {
          Index = 2,
          Mark = Marks.X
        };
        var move4 = new Models.Move
        {
          Index = 3,
          Mark = Marks.X
        };
        var move5 = new Models.Move
        {
          Index = 4,
          Mark = Marks.X
        };
        var move6 = new Models.Move
        {
          Index = 5,
          Mark = Marks.O
        };
        var move7 = new Models.Move
        {
          Index = 6,
          Mark = Marks.O
        };
        var move8 = new Models.Move
        {
          Index = 7,
          Mark = Marks.X
        };
        var move9 = new Models.Move
        {
          Index = 8,
          Mark = Marks.O
        };
        moves = new List<Models.Move> { move, move2, move3, move4, move5, move6, move7, move8, move9 };
        break;
      }
      case MarkPostions.RandomWithBoardNotFull:
      {
        var move = new Models.Move
        {
          Index = 0,
          Mark = Marks.X
        };

        var move2 = new Models.Move
        {
          Index = 1,
          Mark = Marks.O
        };

        var move3 = new Models.Move
        {
          Index = 2,
          Mark = Marks.X
        };

        var move4 = new Models.Move
        {
          Index = 3,
          Mark = Marks.X
        };

        moves = new List<Models.Move> { move, move2, move3, move4 };
        break;
      }
      default:
        throw new ArgumentOutOfRangeException(nameof(positions), positions, null);
    }

    return moves;
  }
}
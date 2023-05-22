using UI.Enums;
using UI.Entities;
using FluentAssertions;

namespace UI.Tests;

public class WinCheckerTests
{
    #region checking rows

    [Fact]
    public void HasWon_WhenThreeMatchingMarksInFirstRow_ShouldReturnTrue()
    {
        // arrange
        Player host = new()
        {
          ConnectionId = "player one connection hub id",
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true
        };
        string GameId = "g00";

        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.X, Marks.X, Marks.X, Marks.X, Marks.X, Marks.O, Marks.X, Marks.X, Marks.X };

        // act
        Game winChecker = new Game(GameId, host);
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeTrue();
    }

    [Fact]
    public void HasWon_WhenThreeUnmatchedMarksInFirstRow_ShouldReturnFalse()
    {
        // arrange
        Player host = new()
        {
          ConnectionId = "player one connection hub id",
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true
        };
        string GameId = "g00";
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.O, Marks.X, Marks.NotSet, Marks.X, Marks.X, Marks.O, Marks.X, Marks.O, Marks.X };

        // act
        Game winChecker = new Game(GameId, host);
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeFalse();
    }

    [Fact]
    public void HasWon_WhenThreeMatchingMarksInSecondRow_ShouldReturnTrue()
    {
        // arrange
        Player host = new()
        {
          ConnectionId = "player one connection hub id",
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true
        };
        string GameId = "g00";
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.X, Marks.NotSet, Marks.O, Marks.X, Marks.X, Marks.X, Marks.O, Marks.O, Marks.X };

        // act
        Game winChecker = new Game(GameId, host);
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeTrue();
    }

    [Fact]
    public void HasWon_WhenThreeUnmatchedMarksInSecondRow_ShouldReturnFalse()
    {
        // arrange
        Player host = new()
        {
          ConnectionId = "player one connection hub id",
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true
        };
        string GameId = "g00";
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.O, Marks.X, Marks.NotSet, Marks.X, Marks.X, Marks.O, Marks.X, Marks.O, Marks.X };

        // act
        Game winChecker = new Game(GameId, host);
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeFalse();
    }

    [Fact]
    public void HasWon_WhenThreeMatchingMarksInThirdRow_ShouldReturnTrue()
    {
        // arrange
        Player host = new()
        {
          ConnectionId = "player one connection hub id",
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true
        };
        string GameId = "g00";
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.X, Marks.NotSet, Marks.O, Marks.NotSet, Marks.X, Marks.O, Marks.X, Marks.X, Marks.X };

        // act
        Game winChecker = new Game(GameId, host);
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeTrue();
    }

    [Fact]
    public void HasWon_WhenThreeUnmatchedMarksInThirdRow_ShouldReturnFalse()
    {
        // arrange
        Player host = new()
        {
          ConnectionId = "player one connection hub id",
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true
        };
        string GameId = "g00";
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.O, Marks.X, Marks.NotSet, Marks.NotSet, Marks.X, Marks.O, Marks.X, Marks.O, Marks.X };

        // act
        Game winChecker = new Game(GameId, host);
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeFalse();
    }

    #endregion

    #region checking columns

    [Fact]
    public void HasWon_WhenThreeMatchingMarksInFirstColumn_ShouldReturnTrue()
    {
        // arrange
        Player host = new()
        {
          ConnectionId = "player one connection hub id",
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true
        };
        string GameId = "g00";
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.X, Marks.NotSet, Marks.O, Marks.X, Marks.X, Marks.O, Marks.X, Marks.NotSet, Marks.O };

        // act
        Game winChecker = new Game(GameId, host);
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeTrue();
    }

    [Fact]
    public void HasWon_WhenThreeUnmatchedMarksInFirstColumn_ShouldReturnFalse()
    {
        // arrange
        Player host = new()
        {
          ConnectionId = "player one connection hub id",
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true
        };
        string GameId = "g00";
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.O, Marks.X, Marks.NotSet, Marks.NotSet, Marks.X, Marks.O, Marks.X, Marks.O, Marks.X };

        // act
        Game winChecker = new Game(GameId, host);
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeFalse();
    }

    [Fact]
    public void HasWon_WhenThreeMatchingMarksInSecondColumn_ShouldReturnTrue()
    {
        // arrange
        Player host = new()
        {
          ConnectionId = "player one connection hub id",
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true
        };
        string GameId = "g00";
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.NotSet, Marks.X, Marks.O, Marks.NotSet, Marks.X, Marks.O, Marks.NotSet, Marks.X, Marks.O };

        // act
        Game winChecker = new Game(GameId, host);
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeTrue();
    }

    [Fact]
    public void HasWon_WhenThreeUnmatchedMarksInSecondColumn_ShouldReturnFalse()
    {
        // arrange
        Player host = new()
        {
          ConnectionId = "player one connection hub id",
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true
        };
        string GameId = "g00";
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.O, Marks.X, Marks.NotSet, Marks.NotSet, Marks.X, Marks.O, Marks.X, Marks.O, Marks.X };

        // act
        Game winChecker = new Game(GameId, host);
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeFalse();
    }

    [Fact]
    public void HasWon_WhenThreeMatchingMarksInThirdColumn_ShouldReturnTrue()
    {
        // arrange
        Player host = new()
        {
          ConnectionId = "player one connection hub id",
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true
        };
        string GameId = "g00";
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.NotSet, Marks.O, Marks.X, Marks.NotSet, Marks.O, Marks.X, Marks.NotSet, Marks.X, Marks.X };

        // act
        Game winChecker = new Game(GameId, host);
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeTrue();
    }

    [Fact]
    public void HasWon_WhenThreeUnmatchedMarksInThirdColumn_ShouldReturnFalse()
    {
        // arrange
        Player host = new()
        {
          ConnectionId = "player one connection hub id",
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true
        };
        string GameId = "g00";
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.O, Marks.X, Marks.NotSet, Marks.NotSet, Marks.X, Marks.O, Marks.X, Marks.O, Marks.X };

        // act
        Game winChecker = new Game(GameId, host);
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeFalse();
    }

    #endregion

    #region checking columns

    [Fact]
    public void HasWon_WhenThreeMatchingMarksForwardDiagonally_ShouldReturnTrue()
    {
        // arrange
        Player host = new()
        {
          ConnectionId = "player one connection hub id",
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true
        };
        string GameId = "g00";
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.X, Marks.NotSet, Marks.O, Marks.O, Marks.X, Marks.O, Marks.X, Marks.NotSet, Marks.X };

        // act
        Game winChecker = new Game(GameId, host);
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeTrue();
    }

    [Fact]
    public void HasWon_WhenThreeUnmatchedMarksForwardDiagonally_ShouldReturnFalse()
    {
        // arrange
        Player host = new()
        {
          ConnectionId = "player one connection hub id",
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true
        };
        string GameId = "g00";
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.O, Marks.X, Marks.NotSet, Marks.NotSet, Marks.X, Marks.O, Marks.X, Marks.O, Marks.X };

        // act
        Game winChecker = new Game(GameId, host);
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeFalse();
    }

    [Fact]
    public void HasWon_WhenThreeMatchingMarksBackwardDiagonally_ShouldReturnTrue()
    {
        // arrange
        Player host = new()
        {
          ConnectionId = "player one connection hub id",
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true
        };
        string GameId = "g00";
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.NotSet, Marks.X, Marks.X, Marks.NotSet, Marks.X, Marks.O, Marks.NotSet, Marks.X, Marks.O };

        // act
        Game winChecker = new Game(GameId, host);
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeTrue();
    }

    [Fact]
    public void HasWon_WhenThreeUnmatchedMarksBackwardDiagonally_ShouldReturnFalse()
    {
        // arrange
        Player host = new()
        {
          ConnectionId = "player one connection hub id",
          Name = "Dan",
          ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
          Mark = Marks.X,
          HasTurn = true
        };
        string GameId = "g00";
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.O, Marks.X, Marks.NotSet, Marks.NotSet, Marks.X, Marks.O, Marks.X, Marks.O, Marks.X };

        // act
        Game winChecker = new Game(GameId, host);
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeFalse();
    }

    #endregion
}
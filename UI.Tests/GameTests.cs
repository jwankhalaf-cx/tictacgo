using UI.Enums;
using UI.Entities;
using FluentAssertions;

namespace UI.Tests;

public class GameTests
{
    #region checking rows

    [Fact]
    public void HasWon_WhenThreeMatchingMarksInFirstRow_ShouldReturnTrue()
    {
        // arrange
        Game game = SetupGame(MarkPostions.RowOne);

        //act
        bool hasWon = game.HasWon();

        // assert
        hasWon.Should().BeTrue();
        game.Host.HasWon.Should().BeTrue();
        game.Guest.HasWon.Should().BeFalse();
    }

    [Fact]
    public void HasWon_WhenThreeMatchingMarksInSecondRow_ShouldReturnTrue()
    {
        // arrange
        Game game = SetupGame(MarkPostions.RowTwo);

        //act
        bool hasWon = game.HasWon();

        // assert
        hasWon.Should().BeTrue();
        game.Host.HasWon.Should().BeTrue();
        game.Guest.HasWon.Should().BeFalse();
    }

    [Fact]
    public void HasWon_WhenThreeMatchingMarksInThirdRow_ShouldReturnTrue()
    {
        // arrange
        Game game = SetupGame(MarkPostions.RowThree);

        //act
        bool hasWon = game.HasWon();

        // assert
        hasWon.Should().BeTrue();
        game.Host.HasWon.Should().BeTrue();
        game.Guest.HasWon.Should().BeFalse();
    }

    #endregion

    #region checking columns

    [Fact]
    public void HasWon_WhenThreeMatchingMarksInFirstColumn_ShouldReturnTrue()
    {
        // arrange
        Game game = SetupGame(MarkPostions.ColumnOne);

        //act
        bool hasWon = game.HasWon();

        // assert
        hasWon.Should().BeTrue();
        game.Host.HasWon.Should().BeTrue();
        game.Guest.HasWon.Should().BeFalse();
    }

    [Fact]
    public void HasWon_WhenThreeMatchingMarksInSecondColumn_ShouldReturnTrue()
    {
        // arrange
        Game game = SetupGame(MarkPostions.ColumnTwo);

        //act
        bool hasWon = game.HasWon();

        // assert
        hasWon.Should().BeTrue();
        game.Host.HasWon.Should().BeTrue();
        game.Guest.HasWon.Should().BeFalse();
    }

    [Fact]
    public void HasWon_WhenThreeMatchingMarksInThirdColumn_ShouldReturnTrue()
    {
        // arrange
        Game game = SetupGame(MarkPostions.ColumnThree);

        //act
        bool hasWon = game.HasWon();

        // assert
        hasWon.Should().BeTrue();
        game.Host.HasWon.Should().BeTrue();
        game.Guest.HasWon.Should().BeFalse();
    }

    #endregion

    #region checking Diagonal

    [Fact]
    public void HasWon_WhenThreeMatchingMarksForwardDiagonally_ShouldReturnTrue()
    {
        // arrange
        Game game = SetupGame(MarkPostions.DiagonalForward);

        //act
        bool hasWon = game.HasWon();

        // assert
        hasWon.Should().BeTrue();
        game.Host.HasWon.Should().BeTrue();
        game.Guest.HasWon.Should().BeFalse();
    }

    [Fact]
    public void HasWon_WhenThreeMatchingMarksBackwardDiagonally_ShouldReturnTrue()
    {
        // arrange
        Game game = SetupGame(MarkPostions.DiagonalBackward);

        //act
        bool hasWon = game.HasWon();

        // assert
        hasWon.Should().BeTrue();
        game.Host.HasWon.Should().BeTrue();
        game.Guest.HasWon.Should().BeFalse();
    }

    #endregion

    #region "checking draw"

    [Fact]
    public void HasWon_WhenNoWinMatchingPatternsExist_ShouldReturnFalse()
    {
        // arrange
        Game game = SetupGame(MarkPostions.NoWin);

        //act
        bool hasWon = game.HasWon();

        // assert
        hasWon.Should().BeFalse();
        game.Host.HasWon.Should().BeFalse();
        game.Guest.HasWon.Should().BeFalse();
    }
    #endregion
    private Game SetupGame(MarkPostions positions)
    {
        Player host = new()
        {
            ConnectionId = "player one connection hub id",
            Name = "Dan",
            ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
            Mark = Marks.X,
            HasTurn = true
        };
        string GameId = "g00";
        Game game = new Game(GameId, host);
        Player guest = new()
        {
            ConnectionId = "player two connection hub id",
            Name = "Emma",
            ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-PNG-Images-HD.png",
            Mark = Marks.O,
            HasTurn = false
        };
        game.AddGuest(guest);
        List<UI.Models.Move> moves = GetMoves(positions);
        foreach (var move in moves)
        {
            game.Move(move);
        }
        return game;
    }
    private List<UI.Models.Move> GetMoves(MarkPostions postions)
    {
        List<UI.Models.Move> moves = new List<UI.Models.Move>();
        if (postions == MarkPostions.ColumnOne)
        {
            var move = new UI.Models.Move
            {
                Index = 0,
                Mark = Marks.X
            };
            var move2 = new UI.Models.Move
            {
                Index = 3,
                Mark = Marks.X
            };
            var move3 = new UI.Models.Move
            {
                Index = 6,
                Mark = Marks.X
            };
            moves = new List<UI.Models.Move> { move, move2, move3 };
        }
        if (postions == MarkPostions.ColumnTwo)
        {
            var move = new UI.Models.Move
            {
                Index = 1,
                Mark = Marks.X
            };
            var move2 = new UI.Models.Move
            {
                Index = 4,
                Mark = Marks.X
            };
            var move3 = new UI.Models.Move
            {
                Index = 7,
                Mark = Marks.X
            };
            moves = new List<UI.Models.Move> { move, move2, move3 };
        }
        if (postions == MarkPostions.ColumnThree)
        {
            var move = new UI.Models.Move
            {
                Index = 2,
                Mark = Marks.X
            };
            var move2 = new UI.Models.Move
            {
                Index = 5,
                Mark = Marks.X
            };
            var move3 = new UI.Models.Move
            {
                Index = 8,
                Mark = Marks.X
            };
            moves = new List<UI.Models.Move> { move, move2, move3 };
        }
        if (postions == MarkPostions.RowOne)
        {
            var move = new UI.Models.Move
            {
                Index = 0,
                Mark = Marks.X
            };
            var move2 = new UI.Models.Move
            {
                Index = 1,
                Mark = Marks.X
            };
            var move3 = new UI.Models.Move
            {
                Index = 2,
                Mark = Marks.X
            };
            moves = new List<UI.Models.Move> { move, move2, move3 };
        }

        if (postions == MarkPostions.RowTwo)
        {
            var move = new UI.Models.Move
            {
                Index = 3,
                Mark = Marks.X
            };
            var move2 = new UI.Models.Move
            {
                Index = 4,
                Mark = Marks.X
            };
            var move3 = new UI.Models.Move
            {
                Index = 5,
                Mark = Marks.X
            };
            moves = new List<UI.Models.Move> { move, move2, move3 };
        }
        if (postions == MarkPostions.RowThree)
        {
            var move = new UI.Models.Move
            {
                Index = 6,
                Mark = Marks.X
            };
            var move2 = new UI.Models.Move
            {
                Index = 7,
                Mark = Marks.X
            };
            var move3 = new UI.Models.Move
            {
                Index = 8,
                Mark = Marks.X
            };
            moves = new List<UI.Models.Move> { move, move2, move3 };
        }
        if (postions == MarkPostions.DiagonalForward)
        {
            var move = new UI.Models.Move
            {
                Index = 2,
                Mark = Marks.X
            };
            var move2 = new UI.Models.Move
            {
                Index = 4,
                Mark = Marks.X
            };
            var move3 = new UI.Models.Move
            {
                Index = 6,
                Mark = Marks.X
            };
            moves = new List<UI.Models.Move> { move, move2, move3 };
        }
        if (postions == MarkPostions.DiagonalBackward)
        {
            var move = new UI.Models.Move
            {
                Index = 0,
                Mark = Marks.X
            };
            var move2 = new UI.Models.Move
            {
                Index = 4,
                Mark = Marks.X
            };
            var move3 = new UI.Models.Move
            {
                Index = 8,
                Mark = Marks.X
            };
            moves = new List<UI.Models.Move> { move, move2, move3 };
        }
        if (postions == MarkPostions.NoWin)
        {
            var move = new UI.Models.Move
            {
                Index = 0,
                Mark = Marks.X
            };
            var move2 = new UI.Models.Move
            {
                Index = 1,
                Mark = Marks.O
            };
            var move3 = new UI.Models.Move
            {
                Index = 2,
                Mark = Marks.X
            };
            var move4 = new UI.Models.Move
            {
                Index = 3,
                Mark = Marks.X
            };
            var move5 = new UI.Models.Move
            {
                Index = 4,
                Mark = Marks.X
            };
            var move6 = new UI.Models.Move
            {
                Index = 5,
                Mark = Marks.O
            };
            var move7 = new UI.Models.Move
            {
                Index = 6,
                Mark = Marks.O
            };
            var move8 = new UI.Models.Move
            {
                Index = 2,
                Mark = Marks.X
            };
            var move9 = new UI.Models.Move
            {
                Index = 2,
                Mark = Marks.O
            };
            moves = new List<UI.Models.Move> { move, move2, move3, move4, move5, move6, move7, move8, move9 };
        }
        return moves;
    }

}
using UI.Enums;
using UI.Services;
using FluentAssertions;

namespace UI.Tests;

public class WinCheckerTests
{
    #region checking rows

    [Fact]
    public void HasWon_WhenThreeMatchingMarksInFirstRow_ShouldReturnTrue()
    {
        // arrange
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.X, Marks.X, Marks.X, Marks.X, Marks.X, Marks.O, Marks.X, Marks.X, Marks.X };

        // act
        WinCheckerService winChecker = new WinCheckerService();
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeTrue();
    }

    [Fact]
    public void HasWon_WhenThreeUnmatchedMarksInFirstRow_ShouldReturnFalse()
    {
        // arrange
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.O, Marks.X, Marks.NotSet, Marks.X, Marks.X, Marks.O, Marks.X, Marks.O, Marks.X };

        // act
        WinCheckerService winChecker = new WinCheckerService();
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeFalse();
    }

    [Fact]
    public void HasWon_WhenThreeMatchingMarksInSecondRow_ShouldReturnTrue()
    {
        // arrange
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.X, Marks.NotSet, Marks.O, Marks.X, Marks.X, Marks.X, Marks.O, Marks.O, Marks.X };

        // act
        WinCheckerService winChecker = new WinCheckerService();
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeTrue();
    }

    [Fact]
    public void HasWon_WhenThreeUnmatchedMarksInSecondRow_ShouldReturnFalse()
    {
        // arrange
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.O, Marks.X, Marks.NotSet, Marks.X, Marks.X, Marks.O, Marks.X, Marks.O, Marks.X };

        // act
        WinCheckerService winChecker = new WinCheckerService();
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeFalse();
    }

    [Fact]
    public void HasWon_WhenThreeMatchingMarksInThirdRow_ShouldReturnTrue()
    {
        // arrange
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.X, Marks.NotSet, Marks.O, Marks.NotSet, Marks.X, Marks.O, Marks.X, Marks.X, Marks.X };

        // act
        WinCheckerService winChecker = new WinCheckerService();
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeTrue();
    }

    [Fact]
    public void HasWon_WhenThreeUnmatchedMarksInThirdRow_ShouldReturnFalse()
    {
        // arrange
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.O, Marks.X, Marks.NotSet, Marks.NotSet, Marks.X, Marks.O, Marks.X, Marks.O, Marks.X };

        // act
        WinCheckerService winChecker = new WinCheckerService();
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
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.X, Marks.NotSet, Marks.O, Marks.X, Marks.X, Marks.O, Marks.X, Marks.NotSet, Marks.O };

        // act
        WinCheckerService winChecker = new WinCheckerService();
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeTrue();
    }

    [Fact]
    public void HasWon_WhenThreeUnmatchedMarksInFirstColumn_ShouldReturnFalse()
    {
        // arrange
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.O, Marks.X, Marks.NotSet, Marks.NotSet, Marks.X, Marks.O, Marks.X, Marks.O, Marks.X };

        // act
        WinCheckerService winChecker = new WinCheckerService();
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeFalse();
    }

    [Fact]
    public void HasWon_WhenThreeMatchingMarksInSecondColumn_ShouldReturnTrue()
    {
        // arrange
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.NotSet, Marks.X, Marks.O, Marks.NotSet, Marks.X, Marks.O, Marks.NotSet, Marks.X, Marks.O };

        // act
        WinCheckerService winChecker = new WinCheckerService();
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeTrue();
    }

    [Fact]
    public void HasWon_WhenThreeUnmatchedMarksInSecondColumn_ShouldReturnFalse()
    {
        // arrange
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.O, Marks.X, Marks.NotSet, Marks.NotSet, Marks.X, Marks.O, Marks.X, Marks.O, Marks.X };

        // act
        WinCheckerService winChecker = new WinCheckerService();
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeFalse();
    }

    [Fact]
    public void HasWon_WhenThreeMatchingMarksInThirdColumn_ShouldReturnTrue()
    {
        // arrange
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.NotSet, Marks.O, Marks.X, Marks.NotSet, Marks.O, Marks.X, Marks.NotSet, Marks.X, Marks.X };

        // act
        WinCheckerService winChecker = new WinCheckerService();
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeTrue();
    }

    [Fact]
    public void HasWon_WhenThreeUnmatchedMarksInThirdColumn_ShouldReturnFalse()
    {
        // arrange
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.O, Marks.X, Marks.NotSet, Marks.NotSet, Marks.X, Marks.O, Marks.X, Marks.O, Marks.X };

        // act
        WinCheckerService winChecker = new WinCheckerService();
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
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.X, Marks.NotSet, Marks.O, Marks.O, Marks.X, Marks.O, Marks.X, Marks.NotSet, Marks.X };

        // act
        WinCheckerService winChecker = new WinCheckerService();
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeTrue();
    }

    [Fact]
    public void HasWon_WhenThreeUnmatchedMarksForwardDiagonally_ShouldReturnFalse()
    {
        // arrange
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.O, Marks.X, Marks.NotSet, Marks.NotSet, Marks.X, Marks.O, Marks.X, Marks.O, Marks.X };

        // act
        WinCheckerService winChecker = new WinCheckerService();
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeFalse();
    }

    [Fact]
    public void HasWon_WhenThreeMatchingMarksBackwardDiagonally_ShouldReturnTrue()
    {
        // arrange
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.NotSet, Marks.X, Marks.X, Marks.NotSet, Marks.X, Marks.O, Marks.NotSet, Marks.X, Marks.O };

        // act
        WinCheckerService winChecker = new WinCheckerService();
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeTrue();
    }

    [Fact]
    public void HasWon_WhenThreeUnmatchedMarksBackwardDiagonally_ShouldReturnFalse()
    {
        // arrange
        Marks markToCheck = Marks.X;
        Marks[] gameBoard = new Marks[] { Marks.O, Marks.X, Marks.NotSet, Marks.NotSet, Marks.X, Marks.O, Marks.X, Marks.O, Marks.X };

        // act
        WinCheckerService winChecker = new WinCheckerService();
        bool hasWon = winChecker.HasWon(markToCheck, gameBoard);

        // assert
        hasWon.Should().BeFalse();
    }

    #endregion
}
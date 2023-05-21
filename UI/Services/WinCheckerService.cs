using UI.Enums;

namespace UI.Services;

public class WinCheckerService : IWinCheckerService
{
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
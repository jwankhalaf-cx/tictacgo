using TicTacGo.Enums;

namespace UI.Services;

public interface IWinCheckerService
{
    bool HasWon(Marks mark, Marks[] gameBoard);
}
using UI.Models;

namespace UI.Services.Interfaces;

public interface IGameEngine
{
  bool GameExists(string gameCode);

  void StartGame(string gameCode, Entities.Player host);

  void JoinGame(string gameCode, Entities.Player guest);

  void LeaveGame(string gameCode, string connectionId);

  Entities.Game? GetGame(string gameCode);

  Entities.Game? MakeMove(string gameCode, Move move);
}
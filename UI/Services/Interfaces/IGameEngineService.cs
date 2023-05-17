using UI.Models;

namespace UI.Services.Interfaces;

public interface IGameEngineService
{
  bool GameExists(string gameCode);

  void StartGame(string gameCode, Player host);

  void JoinGame(string gameCode, Player guest);

  Game? GetGame(string gameCode);

  Game? MakeMove(string gameCode, Move move);
}
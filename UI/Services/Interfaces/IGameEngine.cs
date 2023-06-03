using UI.Models;
using Game = UI.Entities.Game;

namespace UI.Services.Interfaces;

public interface IGameEngine
{
  bool GameExists(string gameCode);

  void StartGame(string gameCode, string connectionId);

  void JoinGame(string gameCode, string connectionId);

  void LeaveGame(string gameCode, string connectionId);

  Game? GetGame(string gameCode);

  Game? MakeMove(string gameCode, Move move);

  Game? SetPlayerName(string gameCode, string connectionId, string name);
}
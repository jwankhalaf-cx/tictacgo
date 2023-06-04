using UI.Models;
using Game = UI.Entities.Game;
using Player = UI.Entities.Player;

namespace UI.Services.Interfaces;

public interface IGameEngine
{
  bool GameExists(string gameCode);

  void StartGame(string gameCode, Player host);

  void JoinGame(string gameCode, Player guest);

  void LeaveGame(string gameCode, string connectionId);

  Game? GetGame(string gameCode);

  Game? MakeMove(string gameCode, Move move);
  Game? ResetGame(string gameCode);
}
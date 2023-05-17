namespace UI.Services.Interfaces;

public interface IGameEngineService
{
  bool GameIsFull(string gameCode);

  void AddPlayerToGame(string gameCode, string playerConnectionId);

  void RemovePlayerFromGame(string gameCode, string playerConnectionId);
}
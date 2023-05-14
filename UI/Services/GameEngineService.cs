using UI.Services.Interfaces;

namespace UI.Services;

public class GameEngineService : IGameEngineService
{
  private int _counter;

  public void IncrementCounter()
  {
    _counter++;
  }

  public int GetCount()
  {
    return _counter;
  }
}
using Microsoft.AspNetCore.SignalR;
using UI.Enums;
using UI.Models;
using UI.Services.Interfaces;

namespace UI.Hubs;

public class GameHub : Hub
{
  private readonly IGameEngineService _gameEngineService;

  public string ActivePlayerClientConnectionId = string.Empty;
  public List<string> ConnectedPlayerIds = new();

  public Marks[] GameBoard =
  {
    Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet, Marks.NotSet,
    Marks.NotSet
  };

  public GameHub(IGameEngineService gameEngineService)
  {
    _gameEngineService = gameEngineService;
  }

  public override async Task OnConnectedAsync()
  {
    Console.WriteLine($"Connection Id: {Context.ConnectionId} is added to ConnectedPlayerIds");

    ConnectedPlayerIds.Add(Context.ConnectionId);
    _gameEngineService.IncrementCounter();

    if (ConnectedPlayerIds.Count == 1)
    {
      Console.WriteLine("only one player connected");
      ActivePlayerClientConnectionId = Context.ConnectionId;
    }
    else
    {
      ActivePlayerClientConnectionId = ConnectedPlayerIds[0];
    }


    await Clients.All.SendAsync("DrawGameBoard", GameBoard, ActivePlayerClientConnectionId);

    await base.OnConnectedAsync();
  }

  public override async Task OnDisconnectedAsync(Exception? exception)
  {
    Console.WriteLine($"Connection Id: {Context.ConnectionId} is removed from ConnectedPlayerIds");

    ConnectedPlayerIds.Remove(Context.ConnectionId);

    await base.OnDisconnectedAsync(exception);
  }

  public async Task MakeMove(MoveModel model)
  {
    Console.WriteLine("Move made:");
    Console.WriteLine(_gameEngineService.GetCount());
    GameBoard[model.CellIndex] = model.SetMark;

    await Clients.All.SendAsync("ReceiveMove", model);
  }
}
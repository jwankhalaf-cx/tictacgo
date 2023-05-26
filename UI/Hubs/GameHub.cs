using Microsoft.AspNetCore.SignalR;
using UI.Enums;
using UI.Mappers;
using UI.Models;
using UI.Services.Interfaces;

namespace UI.Hubs;

public class GameHub : Hub
{
    private readonly IGameEngine _gameEngine;
    private readonly IConverter<Entities.Game, Game> _gameMapper;

    public GameHub(
      IGameEngine gameEngine,
      IConverter<Entities.Game, Game> gameMapper)
    {
        _gameEngine = gameEngine;
        _gameMapper = gameMapper;
    }

    public override async Task OnConnectedAsync()
    {
        if (Context.GetHttpContext()?.GetRouteValue("GameCode") is string gameCode)
        {
            bool gameExists = _gameEngine.GameExists(gameCode);

            if (gameExists)
            {
                Entities.Player guest = new()
                {
                    ConnectionId = Context.ConnectionId,
                    Name = "Emma",
                    ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-PNG-Images-HD.png",
                    Mark = Marks.O,
                    HasTurn = false
                };

                _gameEngine.JoinGame(gameCode, guest);
            }
            else
            {
                Entities.Player host = new()
                {
                    ConnectionId = Context.ConnectionId,
                    Name = "Dan",
                    ImageUrl = "https://www.pngall.com/wp-content/uploads/12/Avatar-Profile-Vector.png",
                    Mark = Marks.X,
                    HasTurn = true
                };

                _gameEngine.StartGame(gameCode, host);
            }

            Entities.Game? game = _gameEngine.GetGame(gameCode);

            if (game is not null)
            {
                Game gameDto = _gameMapper.Convert(game);

                await Clients.All.SendAsync("RenderGame", gameDto);
            }
            else
            {
                await Clients.All.SendAsync("ShowError", "game not found");
            }

            await base.OnConnectedAsync();
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (Context.GetHttpContext()?.GetRouteValue("GameCode") is string gameCode)
        {
            _gameEngine.LeaveGame(gameCode, Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }
    }

    public async Task MakeMove(string gameCode, Move model)
    {
        Entities.Game? game = _gameEngine.MakeMove(gameCode, model);

        if (game is not null)
        {
            // check if last move was a win or draw
            game.HasOutcome(model);

            Game gameDto = _gameMapper.Convert(game);

            await Clients.All.SendAsync("RenderGame", gameDto);
        }
    }
}
using UI.Models;
using Player = UI.Entities.Player;

namespace UI.Mappers;

public class GameEntityToModelMapper : IConverter<Entities.Game, Game>
{
  private readonly IConverter<Player, Models.Player> _playerMapper;

  public GameEntityToModelMapper(IConverter<Player, Models.Player> playerMapper)
  {
    _playerMapper = playerMapper;
  }

  public Game Convert(Entities.Game sourceObject)
  {
    Game game = new Game();
    game.Id = sourceObject.Id;
    if (sourceObject.Host is not null) game.Host = _playerMapper.Convert(sourceObject.Host);
    if (sourceObject.Guest is not null) game.Guest = _playerMapper.Convert(sourceObject.Guest);
    game.Board = sourceObject.GetBoard();

    return game;
  }
}
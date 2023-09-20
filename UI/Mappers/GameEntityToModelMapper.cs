using UI.Entities;

namespace UI.Mappers;

public class GameEntityToModelMapper : IConverter<Game, Models.Game>
{
  private readonly IConverter<Player, Models.Player> _playerMapper;

  public GameEntityToModelMapper(IConverter<Player, Models.Player> playerMapper)
  {
    _playerMapper = playerMapper;
  }

  public Models.Game Convert(Game sourceObject)
  {
    Models.Game game = new Models.Game();
    game.Id = sourceObject.Id;
    if (sourceObject.Host is not null) game.Host = _playerMapper.Convert(sourceObject.Host);
    if (sourceObject.Guest is not null) game.Guest = _playerMapper.Convert(sourceObject.Guest);
    game.Board = sourceObject.GetBoard();
    game.CanStart = sourceObject.CanStart();
    game.Rounds = sourceObject.Rounds;
    return game;
  }
}
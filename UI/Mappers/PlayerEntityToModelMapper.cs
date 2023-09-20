using UI.Entities;

namespace UI.Mappers;

public class PlayerEntityToModelMapper : IConverter<Player, Models.Player>
{
  public Models.Player Convert(Player sourceObject)
  {
    return new Models.Player
    {
      ConnectionId = sourceObject.ConnectionId,
      Name = sourceObject.Name,
      ImageUrl = sourceObject.ImageUrl,
      Mark = sourceObject.Mark,
      HasTurn = sourceObject.HasTurn,
      HasWon = sourceObject.HasWon,
      RoundWon = sourceObject.RoundWon
    };
  }
}
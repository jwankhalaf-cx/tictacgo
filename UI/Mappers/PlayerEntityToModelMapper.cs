using UI.Models;

namespace UI.Mappers;

public class PlayerEntityToModelMapper : IConverter<Entities.Player, Player>
{
  public Player Convert(Entities.Player sourceObject)
  {
    return new Player()
    {
      ConnectionId = sourceObject.ConnectionId,
      Name = sourceObject.Name,
      ImageUrl = sourceObject.ImageUrl,
      Mark = sourceObject.Mark,
      HasTurn = sourceObject.HasTurn
    };
  }
}
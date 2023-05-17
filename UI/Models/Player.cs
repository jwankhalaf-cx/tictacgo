using UI.Enums;

namespace UI.Models;

public class Player
{
  public required string ConnectionId { get; init; }

  public required string Name { get; init; }

  public required string ImageUrl { get; init; }

  public required Marks Mark { get; init; }

  protected bool Equals(Player other)
  {
    return ConnectionId == other.ConnectionId;
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(ConnectionId, Name, ImageUrl, (int)Mark);
  }
}
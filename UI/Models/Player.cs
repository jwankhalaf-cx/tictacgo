using UI.Enums;

namespace UI.Models;

public class Player
{
  public required string ConnectionId { get; init; }

  public required string Name { get; init; }

  public required string ImageUrl { get; init; }

  public required Marks Mark { get; init; }

  public bool HasTurn { get; init; }

  public bool HasWon { get; set; }

  public int RoundWon { get; set; }

}
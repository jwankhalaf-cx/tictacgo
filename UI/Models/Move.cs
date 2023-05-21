using UI.Enums;

namespace UI.Models;

public class Move
{
  public string? ConnectionId { get; set; }

  public int Index { get; init; }

  public Marks Mark { get; init; }
}
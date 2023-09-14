using UI.Enums;

namespace UI.Models;

public class Game
{
  public string? Id { get; set; }

  public Player? Host { get; set; }

  public Player? Guest { get; set; }

  public Marks[]? Board { get; set; }

  public bool CanStart { get; set; }

  public int Rounds { get; set; }
}
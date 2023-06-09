using UI.Enums;

namespace UI.Entities;

public class Player
{
  public required string ConnectionId { get; init; }

  public required string Name { get; set; }

  public required string ImageUrl { get; set; }

  public required Marks Mark { get; init; }

  public bool HasTurn { get; set; }

  public bool HasWon { get; set; }

  public void SetNameAndAvatar(string name)
  {
    Name = string.IsNullOrEmpty(name) ? $"User{Guid.NewGuid().ToString("n")[..6]}" : name;
    ImageUrl = $"https://ui-avatars.com/api/?name={Name}&size=80&length=1&bold=true&format=svg";
  }
}
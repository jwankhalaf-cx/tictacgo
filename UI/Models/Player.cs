using System.Text;
using UI.Enums;

namespace UI.Models;

public class Player
{
  public required string ConnectionId { get; init; }

  public required string Name { get; init; }

  public required string ImageUrl { get; init; }

  public required Marks Mark { get; init; }
  
  public bool HasTurn { get; set; }
  
  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder($"Connection Id: {ConnectionId} | ");
    stringBuilder.Append($"Name: {Name} | ");
    stringBuilder.Append($"ImageUrl: {ImageUrl} | ");
    string markAsString = Mark == Marks.X ? "X" : "O";
    stringBuilder.Append($"Mark: {markAsString} | ");
    stringBuilder.Append($"HasTurn: {HasTurn.ToString()} | ");

    return stringBuilder.ToString();
  }
}
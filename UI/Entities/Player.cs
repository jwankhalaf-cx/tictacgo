using System.Text;
using UI.Enums;

namespace UI.Entities;

public class Player
{
    public required string ConnectionId { get; init; }

    public required string Name { get; init; }

    public required string ImageUrl { get; init; }

    public required Marks Mark { get; init; }

    public bool HasTurn { get; set; }

    public bool HasWon { get; set; }
}
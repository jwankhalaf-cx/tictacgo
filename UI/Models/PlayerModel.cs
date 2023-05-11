using TicTacGo.Enums;

namespace TicTacGo.Models {
  public class PlayerModel
  {
    public string Name { get; set; }

    public string ImageUrl { get; set; }

    public Marks Mark { get; set; }

    public bool HasTurn { get; set; }
  }
}

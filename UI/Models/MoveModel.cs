using UI.Enums;

namespace UI.Models;

public class MoveModel
{
  public int CellIndex { get; init; }

  public Marks SetMark { get; init; }

  public Marks NextMark => SetMark == Marks.X ? Marks.O : Marks.X;
}
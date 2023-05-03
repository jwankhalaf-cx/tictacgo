using TicTacGo.Enums;

namespace TicTacGo.Models
{
    public class CellModel
    {
        public int CellIndex { get; set; }

        public Marks SetMark { get; set; }
    }
}
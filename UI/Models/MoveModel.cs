using TicTacGo.Enums;

namespace TicTacGo.Models
{
    public class MoveModel
    {
        public string PlayerId { get; set; }
        
        public int CellIndex { get; set; }

        public Marks SetMark { get; set; }

        public Marks NextMark
        {
          get
          {
            if (SetMark == Marks.X)
            {
              return Marks.O;
            }

            return Marks.X;
          }
        }
    }
}

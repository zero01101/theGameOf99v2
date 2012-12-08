using System.Collections.Generic;
using theGameOf99v2.Interfaces;

namespace theGameOf99v2
{
    public class SquareCollection : ISquareCollection
    {
        private readonly List<BoardSquare> _squares = new List<BoardSquare>();

        public int Length
        {
            get { return _squares.Count; }
        }

        public void AddSquare(BoardSquare square)
        {
            _squares.Add(square);
        }

        public BoardSquare this[int index]
        {
            get
            {
                if (index < _squares.Count && index >= 0)
                    return _squares[index];
                return null;
            }
        }

        public bool CheckIfWinner(Player player)
        {
            int consecutive = 0;
            foreach (var square in _squares)
            {
                if (square.Occupant == player)
                {
                    consecutive++;
                }
                else
                {
                    consecutive = 0;
                }
                if (consecutive == 5)
                    return true;
            }
            return false;
        }
    }
}
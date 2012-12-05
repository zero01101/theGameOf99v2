using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace theGameOf99v2
{
    public static class BoardBuilder
    {
        public static IList<ISquareCollection> BuildBoard(Action<boardsquare> addAction)
        {
            var tiles = new[] { 
                73, 72, 71, 70, 69, 68, 67, 66, 65, -1, 
                74, 57, 58, 59, 60, 61, 62, 63, 64, 99, 
                75, 56, 21, 20, 19, 18, 17, 36, 37, 98,
                76, 55, 22, 13, 14, 15, 16, 35, 38, 97, 
                77, 54, 23, 12,  1,  4,  5, 34, 39, 96, 
                78, 53, 24, 11,  2,  3,  6, 33, 40, 95,
                79, 52, 25, 10,  9,  8,  7, 32, 41, 94, 
                80, 51, 26, 27, 28, 29, 30, 31, 42, 93, 
                81, 50, 49, 48, 47, 46, 45, 44, 43, 92,
                82, 83, 84, 85, 86, 87, 88, 89, 90, 91
            };

            var squares = new List<ISquareCollection>();
            var rows = BuildRows(tiles, addAction);
            squares.AddRange(rows);
            squares.AddRange(BuildColumns(rows));
            squares.AddRange(BuildLeftDiagnals(rows));
            squares.AddRange(BuildRightDiagnals(rows));
            return squares;
        }

        private static List<ISquareCollection> BuildRows(int[] tiles, Action<boardsquare> squareCreatedAction)
        {
            var rows = new List<ISquareCollection>();
            int count = 0;
            int row = 0;
            while (count < tiles.Length)
            {
                var squareCollection = new SquareCollection();
                var rowTiles = tiles.Skip(count).Take(10).ToList();
                for (int column = 0; column < rowTiles.Count; column++)
                {
                    if (rowTiles[column] == -1) continue;
                    var square = CreateSquare(rowTiles[column], row, column);
                    squareCreatedAction(square);
                    squareCollection.AddSquare(square);
                }
                rows.Add(squareCollection);
                count += 10;
                row++;
            }
            return rows;
        }

        private static  boardsquare CreateSquare(int tile, int row, int column)
        {
            var square = new boardsquare(); //boardsquare object inherits button - see boardsquare.cs
            square.Text = tile.ToString();
            square.squareID = tile;
            square.Size = new System.Drawing.Size(35, 35);
            square.Location = new Point(column * 35, row * 35);
            square.Enabled = false;
            return square;
        }

        private static IEnumerable<ISquareCollection> BuildColumns(List<ISquareCollection> rows)
        {
            var columns = new List<ISquareCollection>();
            for (int column = 0; column < 10; column++)
            {
                var squareCollection = new SquareCollection();
                columns.Add(rows.Aggregate(squareCollection, (newCollection, x) =>
                {
                    var square = x[column];
                    if (square != null)
                        newCollection.AddSquare(square);
                    return newCollection;
                }));
            }
            return columns;
        }

        private static IEnumerable<ISquareCollection> BuildLeftDiagnals(List<ISquareCollection> rows)
        {
            var leftDiagnals = new List<ISquareCollection>();
            int column = 0;
            for (int i = 0; i < 10; i++)
            {
                var diagnal = new SquareCollection();
                foreach (var row in rows)
                {
                    var square = row[column];
                    if (square != null)
                    {
                        diagnal.AddSquare(square);
                    }
                    column++;
                }
                if (diagnal.Length >= 5)
                    leftDiagnals.Add(diagnal);
                else
                    break;
                column = i + 1;
            }
            var reversedRows = rows.ToList();
            reversedRows.Reverse();
            column = 8;
            for (int i = 8; i > 0; i--)
            {
                var diagnal = new SquareCollection();
                foreach (var row in reversedRows)
                {
                    var square = row[column];
                    if (square != null)
                    {
                        diagnal.AddSquare(square);
                    }
                    column--;
                }
                if (diagnal.Length >= 5)
                    leftDiagnals.Add(diagnal);
                else
                    break;
                column = i - 1;
            }
            return leftDiagnals;
        }

        private static IEnumerable<ISquareCollection> BuildRightDiagnals(List<ISquareCollection> rows)
        {
            var rightDiagnals = new List<ISquareCollection>();
            int column = 9;
            for (int i = 9; i > 0; i--)
            {
                var diagnal = new SquareCollection();
                foreach (var row in rows)
                {
                    var square = row[column];
                    if (square != null)
                    {
                        diagnal.AddSquare(square);
                    }
                    column--;
                }
                if (diagnal.Length >= 5)
                    rightDiagnals.Add(diagnal);
                else
                    break;
                column = i - 1;
            }
            var reversedRows = rows.ToList();
            reversedRows.Reverse();
            column = 1;
            for (int i = 1; i < 6; i++)
            {
                var diagnal = new SquareCollection();
                foreach (var row in reversedRows)
                {
                    var square = row[column];
                    if (square != null)
                    {
                        diagnal.AddSquare(square);
                    }
                    column++;
                }
                if (diagnal.Length >= 5)
                    rightDiagnals.Add(diagnal);
                else
                    break;
                column = i + 1;
            }
            return rightDiagnals;
        }
    }

    public interface ISquareCollection
    {
        bool CheckIfWinner(player player);
        boardsquare this[int index] { get; }
    }

    public class SquareCollection : ISquareCollection
    {
        private readonly List<boardsquare> _squares = new List<boardsquare>();

        public int Length { get { return _squares.Count; } }

        public void AddSquare(boardsquare square)
        {
            _squares.Add(square);
        }

        public boardsquare this[int index]
        {
            get
            {
                if (index < _squares.Count && index >= 0)
                    return _squares[index];
                return null;
            }
        }

        public bool CheckIfWinner(player player)
        {
            int consecutive = 0;
            foreach (var square in _squares)
            {
                if (square.occupant == player)
                {
                    consecutive++;
                }
                else
                {
                    consecutive = 0;
                }
            }
            return consecutive == 5;
        }
    }

}
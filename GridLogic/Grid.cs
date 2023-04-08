using System;
using System.Collections.Generic;

namespace AutoBattle
{
    public class Grid
    {
        private List<GridCell> cells = new List<GridCell>();
        private List<GridCell> freeCells = new List<GridCell>();
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public int CellCount => cells.Count;
        private bool changesDetected = true;
        private Random random = new Random();

        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            changesDetected = true;
            Console.WriteLine("The battlefield has been created\n");
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    CreateNewGridCell(column, row);
                }
            }
            freeCells.AddRange(cells);
        }

        private void CreateNewGridCell(int column, int row)
        {
            GridCell newCell = new GridCell(column, row, false);
            newCell.AddStatusChangeListener(GridCellStatusChanged);
            cells.Add(newCell);
        }

        private void GridCellStatusChanged(GridCell cell)
        {
            changesDetected = true;
            if (cell.Occupied)
                freeCells.Remove(cell);
            else
                freeCells.Add(cell);
        }

        public GridCell FindEmptyPosition()
        {
            int[] indexArray = GenerateIndexArray();
            for (int attempt = 0; attempt < freeCells.Count; attempt++)
            {
                int randomResult = random.Next(0, freeCells.Count - attempt);
                int cellIndex = indexArray[randomResult];
                if (!freeCells[cellIndex].Occupied)
                    return freeCells[cellIndex];
                indexArray[randomResult] = indexArray[freeCells.Count - 1 - attempt];
                indexArray[freeCells.Count - 1 - attempt] = cellIndex;
            }
            throw new IndexOutOfRangeException("No free spaces available in the grid");
        }

        private int[] GenerateIndexArray()
        {
            int[] indexArray = new int[freeCells.Count];
            for (int index = 0; index < freeCells.Count; index++)
            {
                indexArray[index] = index;
            }
            return indexArray;
        }

        public bool IsPositionValidAndVacant(int column, int row)
        {
            return (!GetCellAtPosition(column, row)?.Occupied) ?? false;
        }

        public GridCell GetCellAtPosition(int column, int row)
        {
            if (row < 0 || row >= Rows || column < 0 || column >= Columns)
                return null;
            return cells[(Columns * row) + column];
        }

        public GridCell GetCellAtIndex(int index)
        {
            return cells[index];
        }

        public void DrawBattlefieldChanges()
        {
            if (!changesDetected)
                return;
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    GridCell currentCell = cells[(Columns * row) + column];
                    Console.Write($"[{currentCell.DisplayCharacter}]\t");
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
            changesDetected = false;
        }
    }
}
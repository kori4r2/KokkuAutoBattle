using System;
using System.Collections.Generic;

namespace AutoBattle
{
    public class Grid
    {
        private List<GridCell> cells = new List<GridCell>();
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public int CellCount => cells.Count;
        private bool changesDetected = true;
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
        }

        private void CreateNewGridCell(int column, int row)
        {
            GridCell newCell = new GridCell(column, row, false);
            newCell.AddStatusChangeListener(() => changesDetected = true);
            cells.Add(newCell);
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
                    Console.Write($"[{(currentCell.Occupied ? "X" : " ")}]\t");
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
            changesDetected = false;
        }
    }
}
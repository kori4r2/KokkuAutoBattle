using System;
using System.Collections.Generic;

namespace AutoBattle
{
    public class Grid
    {
        public List<GridCell> cells = new List<GridCell>();
        public int Rows;
        public int Columns;
        public Grid(int Rows, int Columns)
        {
            this.Rows = Rows;
            this.Columns = Columns;
            Console.WriteLine("The battlefield has been created\n");
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    GridCell newCell = new GridCell(column, row, false, (Columns * row) + column);
                    Console.Write($"{newCell.Index}\n");
                    cells.Add(newCell);
                }
            }
        }

        public GridCell GetCellAtPosition(int column, int row)
        {
            if (row < 0 || row >= Rows || column < 0 || column >= Columns)
                return null;
            return cells[(Columns * row) + column];
        }

        public void DrawBattlefield()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    GridCell currentCell = cells[(Columns * row) + column];
                    Console.Write($"[{(currentCell.occupied ? "X" : " ")}]\t");
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }
    }
}
using System;
using System.Collections.Generic;

namespace AutoBattle
{
    public class Grid
    {
        public List<GridCell> cells = new List<GridCell>();
        public int xLenght;
        public int yLength;
        public Grid(int Lines, int Columns)
        {
            xLenght = Lines;
            yLength = Columns;
            Console.WriteLine("The battlefield has been created\n");
            for (int i = 0; i < Lines; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    GridCell newCell = new GridCell(j, i, false, (Columns * i) + j);
                    Console.Write($"{newCell.Index}\n");
                    cells.Add(newCell);
                }
            }
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void DrawBattlefield(int Lines, int Columns)
        {
            for (int i = 0; i < Lines; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    GridCell currentCell = new GridCell();
                    if (currentCell.occupied)
                    {
                        Console.Write("[X]\t");
                    }
                    else
                    {
                        Console.Write($"[ ]\t");
                    }
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }
    }
}
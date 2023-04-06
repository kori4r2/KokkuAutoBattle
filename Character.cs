using System;

namespace AutoBattle
{
    public class Character
    {
        public string Name { get; set; }
        public float Health;
        public float BaseDamage;
        public float DamageMultiplier { get; set; }
        public GridCell currentCell;
        public int PlayerIndex;
        public Character Target { get; set; }
        public Character(CharacterClass characterClass)
        {

        }


        public bool TakeDamage(float amount)
        {
            if ((Health -= BaseDamage) <= 0)
            {
                Die();
                return true;
            }
            return false;
        }

        public void Die()
        {
            //TODO >> maybe kill him?
        }

        public void StartTurn(Grid battlefield)
        {
            if (CheckCloseTargets(battlefield))
            {
                Attack(Target);
                return;
            }
            else
            {   // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
                if (currentCell.column > Target.currentCell.column)
                {
                    if (currentCell.column > 0)
                    {
                        currentCell.occupied = false;
                        currentCell = battlefield.GetCellAtPosition(currentCell.column - 1, currentCell.row);
                        currentCell.occupied = true;
                        Console.WriteLine($"Player {PlayerIndex} walked right to row {currentCell.row} and column {currentCell.column}\n");
                        battlefield.DrawBattlefield();
                        return;
                    }
                }
                else if (currentCell.column < Target.currentCell.column)
                {
                    if (currentCell.column < battlefield.Rows - 1)
                    {
                        currentCell.occupied = false;
                        currentCell = battlefield.GetCellAtPosition(currentCell.column + 1, currentCell.row);
                        currentCell.occupied = true;
                        Console.WriteLine($"Player {PlayerIndex} walked left to row {currentCell.row} and column {currentCell.column}\n");
                        battlefield.DrawBattlefield();
                        return;
                    }
                }

                if (currentCell.row > Target.currentCell.row)
                {
                    if (currentCell.row > 0)
                    {
                        currentCell.occupied = false;
                        currentCell = battlefield.GetCellAtPosition(currentCell.column, currentCell.row - 1);
                        currentCell.occupied = true;
                        Console.WriteLine($"Player {PlayerIndex} walked up to row {currentCell.row} and column {currentCell.column}\n");
                        battlefield.DrawBattlefield();
                        return;
                    }
                }
                else if (currentCell.row < Target.currentCell.row)
                {
                    if (currentCell.row < battlefield.Columns - 1)
                    {
                        currentCell.occupied = false;
                        currentCell = battlefield.GetCellAtPosition(currentCell.column, currentCell.row + 1);
                        currentCell.occupied = true;
                        Console.WriteLine($"Player {PlayerIndex} walked down to row {currentCell.row} and column {currentCell.column}\n");
                        battlefield.DrawBattlefield();
                        return;
                    }
                }
            }
        }

        // Check in x and y directions if there is any character close enough to be a target.
        private bool CheckCloseTargets(Grid battlefield)
        {
            bool left = battlefield.GetCellAtPosition(currentCell.column - 1, currentCell.row)?.occupied ?? false;
            bool right = battlefield.GetCellAtPosition(currentCell.column + 1, currentCell.row)?.occupied ?? false;
            bool down = battlefield.GetCellAtPosition(currentCell.column, currentCell.row + 1)?.occupied ?? false;
            bool up = battlefield.GetCellAtPosition(currentCell.column, currentCell.row - 1)?.occupied ?? false;
            return left || right || up || down;
        }

        public void Attack(Character target)
        {
            Random rand = new Random();
            target.TakeDamage(rand.Next(0, (int)BaseDamage));
            Console.WriteLine($"Player {PlayerIndex} is attacking the player {Target.PlayerIndex} and did {BaseDamage} damage\n");
        }
    }
}
using System;

namespace AutoBattle
{
    public class Character
    {
        public string Name { get; set; }
        public float Health = 0;
        public float BaseDamage = 0;
        public float DamageMultiplier { get; set; } = 1;
        public GridCell currentCell = null;
        public int PlayerIndex;
        public Character Target { get; set; } = null;
        private Random random = new Random();
        public Character(CharacterClass characterClass)
        {
        }

        public bool TakeDamage(float amount)
        {
            Health -= amount;
            if (Health <= 0)
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
            if (Health <= 0)
                return;

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
                        currentCell.Occupied = false;
                        currentCell = battlefield.GetCellAtPosition(currentCell.column - 1, currentCell.row);
                        currentCell.Occupied = true;
                        Console.WriteLine($"Player {PlayerIndex} walked right to row {currentCell.row} and column {currentCell.column}\n");
                        return;
                    }
                }
                else if (currentCell.column < Target.currentCell.column)
                {
                    if (currentCell.column < battlefield.Rows - 1)
                    {
                        currentCell.Occupied = false;
                        currentCell = battlefield.GetCellAtPosition(currentCell.column + 1, currentCell.row);
                        currentCell.Occupied = true;
                        Console.WriteLine($"Player {PlayerIndex} walked left to row {currentCell.row} and column {currentCell.column}\n");
                        return;
                    }
                }

                if (currentCell.row > Target.currentCell.row)
                {
                    if (currentCell.row > 0)
                    {
                        currentCell.Occupied = false;
                        currentCell = battlefield.GetCellAtPosition(currentCell.column, currentCell.row - 1);
                        currentCell.Occupied = true;
                        Console.WriteLine($"Player {PlayerIndex} walked up to row {currentCell.row} and column {currentCell.column}\n");
                        return;
                    }
                }
                else if (currentCell.row < Target.currentCell.row)
                {
                    if (currentCell.row < battlefield.Columns - 1)
                    {
                        currentCell.Occupied = false;
                        currentCell = battlefield.GetCellAtPosition(currentCell.column, currentCell.row + 1);
                        currentCell.Occupied = true;
                        Console.WriteLine($"Player {PlayerIndex} walked down to row {currentCell.row} and column {currentCell.column}\n");
                        return;
                    }
                }
            }
        }

        // Check in x and y directions if there is any character close enough to be a target.
        private bool CheckCloseTargets(Grid battlefield)
        {
            bool left = battlefield.GetCellAtPosition(currentCell.column - 1, currentCell.row)?.Occupied ?? false;
            bool right = battlefield.GetCellAtPosition(currentCell.column + 1, currentCell.row)?.Occupied ?? false;
            bool down = battlefield.GetCellAtPosition(currentCell.column, currentCell.row + 1)?.Occupied ?? false;
            bool up = battlefield.GetCellAtPosition(currentCell.column, currentCell.row - 1)?.Occupied ?? false;
            return left || right || up || down;
        }

        public void Attack(Character target)
        {
            float calculatedDamage = random.Next(0, (int)BaseDamage);
            Console.WriteLine($"BaseDamage = {BaseDamage}, rolled for {calculatedDamage}");
            target.TakeDamage(calculatedDamage);
            Console.WriteLine($"Player {PlayerIndex} is attacking the player {Target.PlayerIndex} and did {calculatedDamage} damage\n");
        }
    }
}
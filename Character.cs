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
            currentCell.Occupied = false;
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
                if (currentCell.Column > Target.currentCell.Column)
                {
                    if (currentCell.Column > 0)
                    {
                        currentCell.Occupied = false;
                        currentCell = battlefield.GetCellAtPosition(currentCell.Column - 1, currentCell.Row);
                        currentCell.Occupied = true;
                        Console.WriteLine($"Player {PlayerIndex} walked right to row {currentCell.Row} and column {currentCell.Column}\n");
                        return;
                    }
                }
                else if (currentCell.Column < Target.currentCell.Column)
                {
                    if (currentCell.Column < battlefield.Rows - 1)
                    {
                        currentCell.Occupied = false;
                        currentCell = battlefield.GetCellAtPosition(currentCell.Column + 1, currentCell.Row);
                        currentCell.Occupied = true;
                        Console.WriteLine($"Player {PlayerIndex} walked left to row {currentCell.Row} and column {currentCell.Column}\n");
                        return;
                    }
                }

                if (currentCell.Row > Target.currentCell.Row)
                {
                    if (currentCell.Row > 0)
                    {
                        currentCell.Occupied = false;
                        currentCell = battlefield.GetCellAtPosition(currentCell.Column, currentCell.Row - 1);
                        currentCell.Occupied = true;
                        Console.WriteLine($"Player {PlayerIndex} walked up to row {currentCell.Row} and column {currentCell.Column}\n");
                        return;
                    }
                }
                else if (currentCell.Row < Target.currentCell.Row)
                {
                    if (currentCell.Row < battlefield.Columns - 1)
                    {
                        currentCell.Occupied = false;
                        currentCell = battlefield.GetCellAtPosition(currentCell.Column, currentCell.Row + 1);
                        currentCell.Occupied = true;
                        Console.WriteLine($"Player {PlayerIndex} walked down to row {currentCell.Row} and column {currentCell.Column}\n");
                        return;
                    }
                }
            }
        }

        private bool CheckCloseTargets(Grid battlefield)
        {
            return battlefield.IsPositionValidAndOccupied(currentCell.Column - 1, currentCell.Row)
                || battlefield.IsPositionValidAndOccupied(currentCell.Column + 1, currentCell.Row)
                || battlefield.IsPositionValidAndOccupied(currentCell.Column, currentCell.Row + 1)
                || battlefield.IsPositionValidAndOccupied(currentCell.Column, currentCell.Row - 1);
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
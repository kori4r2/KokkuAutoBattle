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

        public void WalkTO(bool CanWalk)
        {

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
                if (currentCell.xIndex > Target.currentCell.xIndex)
                {
                    if (battlefield.cells.Exists(x => x.Index == currentCell.Index - 1))
                    {
                        currentCell.occupied = false;
                        battlefield.cells[currentCell.Index] = currentCell;
                        currentCell = battlefield.cells.Find(x => x.Index == currentCell.Index - 1);
                        currentCell.occupied = true;
                        battlefield.cells[currentCell.Index] = currentCell;
                        Console.WriteLine($"Player {PlayerIndex} walked left\n");
                        battlefield.DrawBattlefield(5, 5);
                        return;
                    }
                }
                else if (currentCell.xIndex < Target.currentCell.xIndex)
                {
                    if (battlefield.cells.Exists(x => x.Index == currentCell.Index + 1))
                    {
                        currentCell.occupied = false;
                        battlefield.cells[currentCell.Index] = currentCell;
                        currentCell = battlefield.cells.Find(x => x.Index == currentCell.Index + 1);
                        currentCell.occupied = true;
                        battlefield.cells[currentCell.Index] = currentCell;
                        Console.WriteLine($"Player {PlayerIndex} walked right\n");
                        battlefield.DrawBattlefield(5, 5);
                        return;
                    }
                }

                if (currentCell.yIndex > Target.currentCell.yIndex)
                {
                    if (battlefield.cells.Exists(x => x.Index == currentCell.Index - battlefield.xLenght))
                    {
                        currentCell.occupied = false;
                        battlefield.cells[currentCell.Index] = currentCell;
                        currentCell = battlefield.cells.Find(x => x.Index == currentCell.Index - battlefield.xLenght);
                        currentCell.occupied = true;
                        battlefield.cells[currentCell.Index] = currentCell;
                        Console.WriteLine($"Player {PlayerIndex} walked up\n");
                        battlefield.DrawBattlefield(5, 5);
                        return;
                    }
                }
                else if (currentCell.yIndex < Target.currentCell.yIndex)
                {
                    if (battlefield.cells.Exists(x => x.Index == currentCell.Index + battlefield.xLenght))
                    {
                        currentCell.occupied = false;
                        battlefield.cells[currentCell.Index] = currentCell;
                        currentCell = battlefield.cells.Find(x => x.Index == currentCell.Index + battlefield.xLenght);
                        currentCell.occupied = true;
                        battlefield.cells[currentCell.Index] = currentCell;
                        Console.WriteLine($"Player {PlayerIndex} walked down\n");
                        battlefield.DrawBattlefield(5, 5);
                        return;
                    }
                }
            }
        }

        // Check in x and y directions if there is any character close enough to be a target.
        private bool CheckCloseTargets(Grid battlefield)
        {
            bool left = battlefield.cells.Find(x => x.Index == currentCell.Index - 1).occupied;
            bool right = battlefield.cells.Find(x => x.Index == currentCell.Index + 1).occupied;
            bool up = battlefield.cells.Find(x => x.Index == currentCell.Index + battlefield.xLenght).occupied;
            bool down = battlefield.cells.Find(x => x.Index == currentCell.Index - battlefield.xLenght).occupied;

            return left & right & up & down;
        }

        public void Attack(Character target)
        {
            Random rand = new Random();
            target.TakeDamage(rand.Next(0, (int)BaseDamage));
            Console.WriteLine($"Player {PlayerIndex} is attacking the player {Target.PlayerIndex} and did {BaseDamage} damage\n");
        }
    }
}
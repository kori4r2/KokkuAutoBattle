using System;
using System.Collections.ObjectModel;

namespace AutoBattle
{
    public abstract class Character
    {
        public string Name { get; set; }
        public float Health { get; protected set; } = 0;
        public float BaseDamage { get; protected set; } = 0;
        public float DamageMultiplier { get; protected set; } = 1;
        public GridCell CurrentCell { get; protected set; } = null;
        public int CharacterIndex { get; protected set; }
        public CharacterTeam Team { get; protected set; }
        public Character Target { get; set; } = null;
        private Random random = new Random();

        protected Character(GridCell startingPosition, CharacterTeam team, int index)
        {
            Team = team;
            CharacterIndex = index;
            CurrentCell = startingPosition;
            CurrentCell.Occupied = true;
            SetBaseStats();
        }

        protected abstract void SetBaseStats();

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
            CurrentCell.Occupied = false;
        }

        public void StartTurn(Grid battlefield, ReadOnlyCollection<Character> characters)
        {
            if (Health <= 0)
                return;

            FindClosestTarget(characters);
            if (IsAdjacentToTarget(battlefield))
                Attack(Target);
            else
                MoveTowardsTarget(battlefield);
        }

        protected void FindClosestTarget(ReadOnlyCollection<Character> characterList)
        {
            int closestDistance = int.MaxValue;
            foreach (Character character in characterList)
            {
                if (character.Team == Team)
                    continue;
                int distance = DistanceToCharacter(character);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    Target = character;
                }
            }
        }

        protected int DistanceToCharacter(Character other)
        {
            return Math.Abs(CurrentCell.Row - other.CurrentCell.Row) + Math.Abs(CurrentCell.Column - other.CurrentCell.Column);
        }

        protected bool IsAdjacentToTarget(Grid battlefield)
        {
            return DistanceToCharacter(Target) < 2;
        }

        protected void Attack(Character target)
        {
            float calculatedDamage = random.Next(0, (int)BaseDamage);
            Console.WriteLine($"BaseDamage = {BaseDamage}, rolled for {calculatedDamage}");
            target.TakeDamage(calculatedDamage);
            Console.WriteLine($"Player {CharacterIndex} is attacking the player {Target.CharacterIndex} and did {calculatedDamage} damage\n");
        }

        protected void MoveTowardsTarget(Grid battlefield)
        {
            if (ShouldMoveRight(battlefield))
            {
                MoveToPosition(battlefield, CurrentCell.Column - 1, CurrentCell.Row);
                Console.WriteLine($"Player {CharacterIndex} walked right to row {CurrentCell.Row} and column {CurrentCell.Column}\n");
                return;
            }
            if (ShouldMoveLeft(battlefield))
            {
                MoveToPosition(battlefield, CurrentCell.Column + 1, CurrentCell.Row);
                Console.WriteLine($"Player {CharacterIndex} walked left to row {CurrentCell.Row} and column {CurrentCell.Column}\n");
                return;
            }
            if (ShouldMoveUp(battlefield))
            {
                MoveToPosition(battlefield, CurrentCell.Column, CurrentCell.Row - 1);
                Console.WriteLine($"Player {CharacterIndex} walked up to row {CurrentCell.Row} and column {CurrentCell.Column}\n");
                return;
            }
            if (ShouldMoveDown(battlefield))
            {
                MoveToPosition(battlefield, CurrentCell.Column, CurrentCell.Row + 1);
                Console.WriteLine($"Player {CharacterIndex} walked down to row {CurrentCell.Row} and column {CurrentCell.Column}\n");
                return;
            }
        }

        private bool ShouldMoveRight(Grid battlefield)
        {
            return CurrentCell.Column > Target.CurrentCell.Column
                && battlefield.IsPositionValidAndVacant(CurrentCell.Column - 1, CurrentCell.Row);
        }

        private bool ShouldMoveLeft(Grid battlefield)
        {
            return CurrentCell.Column < Target.CurrentCell.Column
                && battlefield.IsPositionValidAndVacant(CurrentCell.Column + 1, CurrentCell.Row);
        }

        private bool ShouldMoveUp(Grid battlefield)
        {
            return CurrentCell.Row > Target.CurrentCell.Row
                && battlefield.IsPositionValidAndVacant(CurrentCell.Column, CurrentCell.Row - 1);
        }

        private bool ShouldMoveDown(Grid battlefield)
        {
            return CurrentCell.Row < Target.CurrentCell.Row
                && battlefield.IsPositionValidAndVacant(CurrentCell.Column, CurrentCell.Row + 1);
        }

        private void MoveToPosition(Grid battlefield, int column, int row)
        {
            CurrentCell.Occupied = false;
            CurrentCell = battlefield.GetCellAtPosition(column, row);
            CurrentCell.Occupied = true;
        }
    }
}
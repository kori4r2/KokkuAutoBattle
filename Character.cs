using System;
using System.Collections.ObjectModel;

namespace AutoBattle
{
    public abstract class Character
    {
        public string Name => $"{Team}{MyClass}({CharacterIndex})";
        public float Health { get; protected set; } = 0;
        public float BaseDamage { get; protected set; } = 0;
        public float DamageMultiplier { get; protected set; } = 1;
        public GridCell CurrentCell { get; protected set; } = null;
        public int CharacterIndex { get; protected set; }
        protected char DisplayChar => Team == CharacterTeam.Player ? 'P' : Team == CharacterTeam.Enemy ? 'E' : 'X';
        protected abstract CharacterClass MyClass { get; }
        public CharacterTeam Team { get; protected set; }
        public Character Target { get; set; } = null;
        protected Random random = new Random();

        protected Character(GridCell startingPosition, CharacterTeam team, int index)
        {
            Team = team;
            CharacterIndex = index;
            CurrentCell = startingPosition;
            CurrentCell.Occupy(DisplayChar);
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
            CurrentCell.Vacate();
            Console.WriteLine($"{Name} has died");
        }

        public void StartTurn(Grid battlefield, ReadOnlyCollection<Character> characters)
        {
            if (Health <= 0)
                return;

            TargetClosestEnemyAlive(characters);
            if (DistanceToCharacter(Target) < 2)
                Attack(Target);
            else
                MoveTowardsTarget(battlefield);
        }

        protected void TargetClosestEnemyAlive(ReadOnlyCollection<Character> characterList)
        {
            int closestDistance = int.MaxValue;
            foreach (Character character in characterList)
            {
                if (character.Team == Team || character.Health <= 0)
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

        protected void Attack(Character target)
        {
            if (target.Health <= 0)
                return;
            float calculatedDamage = random.Next(0, (int)BaseDamage);
            Console.WriteLine($"BaseDamage = {BaseDamage}, rolled {calculatedDamage}");
            Console.WriteLine($"{Name} is attacking {Target.Name} for {calculatedDamage} damage");
            target.TakeDamage(calculatedDamage);
            Console.WriteLine(Environment.NewLine);
        }

        protected void MoveTowardsTarget(Grid battlefield)
        {
            if (ShouldMoveRight(battlefield))
            {
                MoveToPosition(battlefield, CurrentCell.Column + 1, CurrentCell.Row);
                LogMovement("right");
                return;
            }
            if (ShouldMoveLeft(battlefield))
            {
                MoveToPosition(battlefield, CurrentCell.Column - 1, CurrentCell.Row);
                LogMovement("left");
                return;
            }
            if (ShouldMoveUp(battlefield))
            {
                MoveToPosition(battlefield, CurrentCell.Column, CurrentCell.Row - 1);
                LogMovement("up");
                return;
            }
            if (ShouldMoveDown(battlefield))
            {
                MoveToPosition(battlefield, CurrentCell.Column, CurrentCell.Row + 1);
                LogMovement("down");
                return;
            }
        }

        private void LogMovement(string directionString)
        {
            Console.WriteLine($"{Name} walked {directionString} to row {CurrentCell.Row} and column {CurrentCell.Column}\n");
        }

        private bool ShouldMoveRight(Grid battlefield)
        {
            return CurrentCell.Column < Target.CurrentCell.Column
                && battlefield.IsPositionValidAndVacant(CurrentCell.Column + 1, CurrentCell.Row);
        }

        private bool ShouldMoveLeft(Grid battlefield)
        {
            return CurrentCell.Column > Target.CurrentCell.Column
                && battlefield.IsPositionValidAndVacant(CurrentCell.Column - 1, CurrentCell.Row);
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
            CurrentCell.Vacate();
            CurrentCell = battlefield.GetCellAtPosition(column, row);
            CurrentCell.Occupy(DisplayChar);
        }
    }
}
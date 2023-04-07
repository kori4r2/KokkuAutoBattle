using System;

namespace AutoBattle
{
    public class CharacterFactory
    {
        private int unitIndex = 0;

        public Character CreatePlayerCharacter(CharacterClass characterClass, GridCell startingPosition)
        {
            return CreateCharacter(characterClass, startingPosition, CharacterTeam.Player);
        }

        public Character CreateEnemyCharacter(CharacterClass characterClass, GridCell startingPosition)
        {
            return CreateCharacter(characterClass, startingPosition, CharacterTeam.Enemy);
        }

        private Character CreateCharacter(CharacterClass characterClass, GridCell startingPosition, CharacterTeam team)
        {
            return characterClass switch
            {
                CharacterClass.Archer => new Archer(startingPosition, team, unitIndex++),
                CharacterClass.Paladin => new Paladin(startingPosition, team, unitIndex++),
                CharacterClass.Warrior => new Warrior(startingPosition, team, unitIndex++),
                CharacterClass.Cleric => new Cleric(startingPosition, team, unitIndex++),
                _ => throw new NotImplementedException($"Class {characterClass} creation is not implemented"),
            };
        }
    }
}
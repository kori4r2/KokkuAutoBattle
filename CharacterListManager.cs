
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AutoBattle
{
    public class CharacterListManager
    {
        private Character PlayerCharacter;
        public bool PlayerHasUnitsAlive => PlayerCharacter.Health > 0;
        private Character EnemyCharacter;
        public bool EnemyHasUnitsAlive => EnemyCharacter.Health > 0;
        private CharacterFactory characterFactory = new CharacterFactory();
        private List<Character> allCharacters = new List<Character>();
        public ReadOnlyCollection<Character> Characters => allCharacters.AsReadOnly();
        Random random = new Random();

        public void CreatePlayerCharacter(CharacterClass playerClass, GridCell startingPosition)
        {
            Console.WriteLine($"Player Class Choice: {playerClass}");
            PlayerCharacter = characterFactory.CreatePlayerCharacter(playerClass, startingPosition);
            allCharacters.Add(PlayerCharacter);
            Console.Write($"Player Characher placed at row {PlayerCharacter.CurrentCell.Row} and column {PlayerCharacter.CurrentCell.Column}\n");
        }

        public void CreateEnemyCharacter(CharacterClass enemyClass, GridCell startingPosition)
        {
            Console.WriteLine($"Enemy Class Choice: {enemyClass}");
            EnemyCharacter = characterFactory.CreateEnemyCharacter(enemyClass, startingPosition);
            allCharacters.Add(EnemyCharacter);
            Console.Write($"Enemy Characher placed at row {EnemyCharacter.CurrentCell.Row} and column {EnemyCharacter.CurrentCell.Column}\n");
        }

        public void ShuffleList()
        {
            for (int index = 0; index < allCharacters.Count; index++)
            {
                int randomIndex = random.Next(index, allCharacters.Count);
                Character selectedCharacter = allCharacters[randomIndex];
                allCharacters[randomIndex] = allCharacters[index];
                allCharacters[index] = selectedCharacter;
            }
        }
    }
}
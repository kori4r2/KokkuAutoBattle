
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AutoBattle
{
    public class CharacterListManager
    {
        private CharacterFactory characterFactory = new CharacterFactory();
        private List<Character> allCharacters = new List<Character>();
        public ReadOnlyCollection<Character> Characters => allCharacters.AsReadOnly();
        private List<Character> playerCharacters = new List<Character>();
        public bool PlayerHasUnitsAlive()
        {
            foreach (Character character in playerCharacters)
            {
                if (character.Health > 0)
                    return true;
            }
            return false;
        }
        private List<Character> enemyCharacters = new List<Character>();
        public bool EnemyHasUnitsAlive()
        {
            foreach (Character character in enemyCharacters)
            {
                if (character.Health > 0)
                    return true;
            }
            return false;
        }
        Random random = new Random();

        public void CreatePlayerCharacter(CharacterClass playerClass, GridCell startingPosition)
        {
            Console.WriteLine($"Player Class Choice: {playerClass}");
            Character newPlayerCharater = characterFactory.CreatePlayerCharacter(playerClass, startingPosition);
            playerCharacters.Add(newPlayerCharater);
            allCharacters.Add(newPlayerCharater);
            Console.Write($"Player Character placed at row {newPlayerCharater.CurrentCell.Row} and column {newPlayerCharater.CurrentCell.Column}\n");
        }

        public void CreateEnemyCharacter(CharacterClass enemyClass, GridCell startingPosition)
        {
            Console.WriteLine($"Enemy Class Choice: {enemyClass}");
            Character newEnemyCharacter = characterFactory.CreateEnemyCharacter(enemyClass, startingPosition);
            enemyCharacters.Add(newEnemyCharacter);
            allCharacters.Add(newEnemyCharacter);
            Console.Write($"Enemy Character placed at row {newEnemyCharacter.CurrentCell.Row} and column {newEnemyCharacter.CurrentCell.Column}\n");
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
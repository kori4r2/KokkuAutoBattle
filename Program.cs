using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoBattle
{
    internal class Program
    {
        private Grid grid = new Grid(5, 5);
        private Character PlayerCharacter;
        private Character EnemyCharacter;
        private List<Character> AllCharacters = new List<Character>();
        private Random random = new Random();

        private static void Main()
        {
            Program program = new Program();
            program.Setup();
            program.StartGame();
        }

        private void Setup()
        {
            GetAndCreatePlayerCharacter();
            CreateEnemyCharacter();
            PopulateListAndSetTargets();
            AlocatePlayers();
            AlocateEnemyCharacter();
            grid.DrawBattlefield();
        }

        private void GetAndCreatePlayerCharacter()
        {
            bool playerCharacterCreated = false;
            do
            {
                //asks for the player to choose between four possible classes via console.
                Console.WriteLine("Choose Between One of these Classes:\n");
                Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
                //store the player choice in a variable
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreatePlayerCharacter(int.Parse(choice));
                        playerCharacterCreated = true;
                        break;
                    case "2":
                        CreatePlayerCharacter(int.Parse(choice));
                        playerCharacterCreated = true;
                        break;
                    case "3":
                        CreatePlayerCharacter(int.Parse(choice));
                        playerCharacterCreated = true;
                        break;
                    case "4":
                        CreatePlayerCharacter(int.Parse(choice));
                        playerCharacterCreated = true;
                        break;
                }
            } while (!playerCharacterCreated);
        }

        private void CreatePlayerCharacter(int classIndex)
        {
            CharacterClass characterClass = (CharacterClass)classIndex;
            Console.WriteLine($"Player Class Choice: {characterClass}");
            PlayerCharacter = new Character(characterClass);
            PlayerCharacter.Health = 100;
            PlayerCharacter.BaseDamage = 20;
            PlayerCharacter.PlayerIndex = 0;
        }

        private void CreateEnemyCharacter()
        {
            //randomly choose the enemy class and set up vital variables
            int randomInteger = random.Next(1, 4);
            CharacterClass enemyClass = (CharacterClass)randomInteger;
            Console.WriteLine($"Enemy Class Choice: {enemyClass}");
            EnemyCharacter = new Character(enemyClass);
            EnemyCharacter.Health = 100;
            EnemyCharacter.BaseDamage = 20;
            EnemyCharacter.PlayerIndex = 1;
        }

        private void PopulateListAndSetTargets()
        {
            EnemyCharacter.Target = PlayerCharacter;
            PlayerCharacter.Target = EnemyCharacter;
            AllCharacters.Add(PlayerCharacter);
            AllCharacters.Add(EnemyCharacter);
        }

        private void AlocatePlayers()
        {
            AlocatePlayerCharacter();
        }

        private void AlocatePlayerCharacter()
        {
            do
            {
                int randomIndex = random.Next(0, grid.cells.Count);
                GridCell RandomLocation = grid.cells.ElementAt(randomIndex);
                if (!RandomLocation.occupied)
                {
                    PlayerCharacter.currentCell = RandomLocation;
                    RandomLocation.occupied = true;
                    Console.Write($"{randomIndex}\n");
                    return;
                }
            } while (true);
        }

        private void AlocateEnemyCharacter()
        {
            do
            {
                int randomIndex = random.Next(0, grid.cells.Count);
                GridCell RandomLocation = grid.cells.ElementAt(randomIndex);
                if (!RandomLocation.occupied)
                {
                    EnemyCharacter.currentCell = RandomLocation;
                    RandomLocation.occupied = true;
                    Console.Write($"{randomIndex}\n");
                    return;
                }
            } while (true);
        }

        public void StartGame()
        {
            //AllPlayers.Sort();  
            do
            {
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.WriteLine("Click on any key to start the next turn...\n");
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.ReadKey();
                StartCharacterTurns();
            } while (!CheckGameEnd());
        }

        private void StartCharacterTurns()
        {
            foreach (Character character in AllCharacters)
            {
                character.StartTurn(grid);
            }
        }

        private bool CheckGameEnd()
        {
            if (PlayerCharacter.Health <= 0)
            {
                return true;
            }
            else if (EnemyCharacter.Health <= 0)
            {
                Console.Write(Environment.NewLine + Environment.NewLine);

                // endgame?

                Console.Write(Environment.NewLine + Environment.NewLine);

                return true;
            }
            return false;
        }
    }
}
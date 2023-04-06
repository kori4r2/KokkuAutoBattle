using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoBattle
{
    internal class Program
    {
        private Grid grid;
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
            CreateGrid();
            GetAndCreatePlayerCharacter();
            CreateEnemyCharacter();
            PopulateListAndSetTargets();
            AlocatePlayers();
            AlocateEnemyCharacter();
            grid.DrawBattlefield();
        }

        private void CreateGrid()
        {
            int numberParsed;
            int rowNumber;
            int columnNumber;
            Console.WriteLine("Choose number of rows for the grid [1-999999]: ");
            numberParsed = ReadValidNumberFromConsole();
            rowNumber = Math.Clamp(numberParsed, 1, 999999);
            Console.WriteLine("Choose number of columns for the grid [1-999999]: ");
            numberParsed = ReadValidNumberFromConsole();
            columnNumber = Math.Clamp(numberParsed, 1, 999999);
            Console.WriteLine($"Creating grid with {rowNumber} row{(rowNumber > 1 ? "s" : "")} and {columnNumber} column{(columnNumber > 1 ? "s" : "")}");
            grid = new Grid(rowNumber, columnNumber);
        }

        private static int ReadValidNumberFromConsole()
        {
            int numberParsed;
            string lineRead = Console.ReadLine();
            while (!int.TryParse(lineRead, out numberParsed))
            {
                Console.WriteLine("Please input a valid number: ");
                lineRead = Console.ReadLine();
            }
            return numberParsed;
        }

        private void GetAndCreatePlayerCharacter()
        {
            bool playerCharacterCreated = false;
            do
            {
                Console.WriteLine("Choose Between One of these Classes:\n");
                Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
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
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.WriteLine("Game Over, Player Character has died");
                return true;
            }
            else if (EnemyCharacter.Health <= 0)
            {
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.WriteLine("Game Over, Enemy Character has died");
                return true;
            }
            return false;
        }
    }
}
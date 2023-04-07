using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AutoBattle
{
    internal class Program
    {
        private Grid grid;
        private Character PlayerCharacter;
        private Character EnemyCharacter;
        private CharacterFactory characterFactory = new CharacterFactory();
        private List<Character> AllCharacters = new List<Character>();
        private ReadOnlyCollection<Character> ReadOnlyCharacterList;
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
            ReadOnlyCharacterList = AllCharacters.AsReadOnly();
            grid.DrawBattlefieldChanges();
        }

        private void CreateGrid()
        {
            int numberParsed;
            int rowNumber;
            int columnNumber;
            Console.WriteLine("Choose number of rows for the grid [2-999999]: ");
            numberParsed = ReadValidNumberFromConsole();
            rowNumber = Math.Clamp(numberParsed, 2, 999999);
            Console.WriteLine("Choose number of columns for the grid [2-999999]: ");
            numberParsed = ReadValidNumberFromConsole();
            columnNumber = Math.Clamp(numberParsed, 2, 999999);
            Console.WriteLine($"Creating grid with {rowNumber} row{(rowNumber > 1 ? "s" : "")} and {columnNumber} column{(columnNumber > 1 ? "s" : "")}...");
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
                Console.WriteLine($"[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
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
            GridCell emptyCell = FindEmptyPosition();
            PlayerCharacter = characterFactory.CreatePlayerCharacter(characterClass, emptyCell);
            AllCharacters.Add(PlayerCharacter);
            Console.Write($"Player Characher placed at row {PlayerCharacter.CurrentCell.Row} and column {PlayerCharacter.CurrentCell.Column}\n");
        }

        private GridCell FindEmptyPosition()
        {
            do
            {
                int randomIndex = random.Next(0, grid.CellCount);
                GridCell RandomLocation = grid.GetCellAtIndex(randomIndex);
                if (!RandomLocation.Occupied)
                    return RandomLocation;
            } while (true);
        }

        private void CreateEnemyCharacter()
        {
            //randomly choose the enemy class and set up vital variables
            int randomInteger = random.Next(1, 4);
            CharacterClass enemyClass = (CharacterClass)randomInteger;
            Console.WriteLine($"Enemy Class Choice: {enemyClass}");
            GridCell emptyCell = FindEmptyPosition();
            EnemyCharacter = characterFactory.CreateEnemyCharacter(enemyClass, emptyCell);
            AllCharacters.Add(EnemyCharacter);
            Console.Write($"Enemy Characher placed at row {EnemyCharacter.CurrentCell.Row} and column {EnemyCharacter.CurrentCell.Column}\n");
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
                character.StartTurn(grid, ReadOnlyCharacterList);
                grid.DrawBattlefieldChanges();
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
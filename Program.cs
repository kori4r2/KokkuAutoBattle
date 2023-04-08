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
        private ClassesInfo classesInfo = new ClassesInfo();

        private static void Main()
        {
            Program program = new Program();
            program.Setup();
            program.StartGame();
        }

        private void Setup()
        {
            CreateGrid();
            CreateCharacters();
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

        private void CreateCharacters()
        {
            CharacterClass playerCharacterClass = ReadPlayerCharacterClass();
            CreatePlayerCharacter(playerCharacterClass);
            CreateEnemyCharacter(classesInfo.GetRandomClass());
            ReadOnlyCharacterList = AllCharacters.AsReadOnly();
        }

        private CharacterClass ReadPlayerCharacterClass()
        {
            do
            {
                Console.WriteLine("Choose Between One of these Classes:\n");
                Console.WriteLine(classesInfo.GetReadableClassList(", "));
                string choice = Console.ReadLine();
                if (classesInfo.IsValidClassString(choice))
                    return classesInfo.ParseValidString(choice);
            } while (true);
        }

        private void CreatePlayerCharacter(CharacterClass playerClass)
        {
            Console.WriteLine($"Player Class Choice: {playerClass}");
            GridCell emptyCell = grid.FindEmptyPosition();
            PlayerCharacter = characterFactory.CreatePlayerCharacter(playerClass, emptyCell);
            AllCharacters.Add(PlayerCharacter);
            Console.Write($"Player Characher placed at row {PlayerCharacter.CurrentCell.Row} and column {PlayerCharacter.CurrentCell.Column}\n");
        }

        private void CreateEnemyCharacter(CharacterClass enemyClass)
        {
            Console.WriteLine($"Enemy Class Choice: {enemyClass}");
            GridCell emptyCell = grid.FindEmptyPosition();
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
                Console.WriteLine("Player Character has died, Defeat...");
                Console.Write(Environment.NewLine);
                return true;
            }
            else if (EnemyCharacter.Health <= 0)
            {
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.WriteLine("Enemy Character has died, Victory!");
                Console.Write(Environment.NewLine);
                return true;
            }
            return false;
        }
    }
}
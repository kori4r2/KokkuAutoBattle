using System;

namespace AutoBattle
{
    internal class Program
    {
        private Grid grid;
        private ClassesInfo classesInfo = new ClassesInfo();
        private CharacterListManager characterList = new CharacterListManager();

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
            characterList.CreatePlayerCharacter(ReadPlayerCharacterClass(), grid.FindEmptyPosition());
            characterList.CreateEnemyCharacter(classesInfo.GetRandomClass(), grid.FindEmptyPosition());
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
            foreach (Character character in characterList.Characters)
            {
                character.StartTurn(grid, characterList.Characters);
                grid.DrawBattlefieldChanges();
            }
        }

        private bool CheckGameEnd()
        {
            if (!characterList.EnemyHasUnitsAlive)
            {
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.WriteLine("Enemy has no units alive, Victory!");
                Console.Write(Environment.NewLine);
                return true;
            }
            else if (!characterList.PlayerHasUnitsAlive)
            {
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.WriteLine("Player has no units alive, Defeat...");
                Console.Write(Environment.NewLine);
                return true;
            }
            return false;
        }
    }
}
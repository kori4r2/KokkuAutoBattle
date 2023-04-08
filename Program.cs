using System;

namespace AutoBattle
{
    internal class Program
    {
        private const int minTeamSize = 1;
        private const int minGridSize = 2;
        private const int maxGridSize = 50;
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
            Console.WriteLine(Environment.NewLine);
            grid.DrawBattlefieldChanges();
        }

        private void CreateGrid()
        {
            int numberParsed;
            int rowNumber;
            int columnNumber;
            Console.Write($"Choose number of rows for the grid [{minGridSize}-{maxGridSize}]: ");
            numberParsed = ReadValidNumberFromConsole();
            rowNumber = Math.Clamp(numberParsed, minGridSize, maxGridSize);
            Console.Write($"Choose number of columns for the grid [{minGridSize}-{maxGridSize}]: ");
            numberParsed = ReadValidNumberFromConsole();
            columnNumber = Math.Clamp(numberParsed, minGridSize, maxGridSize);
            Console.WriteLine($"Creating grid with {rowNumber} row{(rowNumber > 1 ? "s" : "")} and {columnNumber} column{(columnNumber > 1 ? "s" : "")}...");
            grid = new Grid(rowNumber, columnNumber);
        }

        private static int ReadValidNumberFromConsole()
        {
            int numberParsed;
            string lineRead = Console.ReadLine();
            while (!int.TryParse(lineRead, out numberParsed))
            {
                Console.Write("Please input a valid number: ");
                lineRead = Console.ReadLine();
            }
            return numberParsed;
        }

        private void CreateCharacters()
        {
            int teamSize = GetTeamSize();
            CreatePlayerTeam(teamSize);
            CreateEnemyTeam(teamSize);
            characterList.ShuffleList();
        }

        private int GetTeamSize()
        {
            int teamSize = minTeamSize;
            // The total number of units should ideally occupy at most half of the grid
            int maxTeamSize = Math.Max(minTeamSize, grid.CellCount / 4);
            if (maxTeamSize > teamSize)
            {
                Console.Write($"Choose team size [{minTeamSize}-{maxTeamSize}]: ");
                teamSize = ReadValidNumberFromConsole();
                teamSize = Math.Clamp(teamSize, minTeamSize, maxTeamSize);
            }
            Console.WriteLine($"Using team size of {teamSize}");
            return teamSize;
        }

        private void CreatePlayerTeam(int teamSize)
        {
            for (int count = 0; count < teamSize; count++)
                characterList.CreatePlayerCharacter(ReadPlayerCharacterClass(), grid.FindEmptyPosition());
        }

        private CharacterClass ReadPlayerCharacterClass()
        {
            do
            {
                Console.WriteLine("Choose Between One of these Classes:");
                Console.WriteLine(classesInfo.GetReadableClassList(", "));
                string choice = Console.ReadLine();
                if (classesInfo.IsValidClassString(choice))
                    return classesInfo.ParseValidString(choice);
            } while (true);
        }

        private void CreateEnemyTeam(int teamSize)
        {
            for (int count = 0; count < teamSize; count++)
                characterList.CreateEnemyCharacter(classesInfo.GetRandomClass(), grid.FindEmptyPosition());
        }

        public void StartGame()
        {
            do
            {
                StartCharacterTurns();
            } while (!CheckGameEnd());
        }

        private void StartCharacterTurns()
        {
            foreach (Character character in characterList.Characters)
            {
                if (character.Health <= 0)
                    continue;
                Console.WriteLine();
                Console.WriteLine("Click on any key to start the next unit's turn...\n");
                Console.ReadKey();
                character.StartTurn(grid, characterList.Characters);
                grid.DrawBattlefieldChanges();
            }
        }

        private bool CheckGameEnd()
        {
            if (!characterList.EnemyHasUnitsAlive())
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Enemy has no units alive, Victory!");
                Console.WriteLine();
                return true;
            }
            else if (!characterList.PlayerHasUnitsAlive())
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Player has no units alive, Defeat...");
                Console.WriteLine();
                return true;
            }
            return false;
        }
    }
}
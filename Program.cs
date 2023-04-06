﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoBattle
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Grid grid = new Grid(5, 5);
            CharacterClass playerCharacterClass;
            GridCell PlayerCurrentLocation;
            GridCell EnemyCurrentLocation;
            Character PlayerCharacter;
            Character EnemyCharacter;
            List<Character> AllPlayers = new List<Character>();
            int currentTurn = 0;
            int numberOfPossibleTiles = grid.cells.Count;
            Setup();

            void Setup()
            {
                GetPlayerChoice();
            }

            void GetPlayerChoice()
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
                        break;
                    case "2":
                        CreatePlayerCharacter(int.Parse(choice));
                        break;
                    case "3":
                        CreatePlayerCharacter(int.Parse(choice));
                        break;
                    case "4":
                        CreatePlayerCharacter(int.Parse(choice));
                        break;
                    default:
                        GetPlayerChoice();
                        break;
                }
            }

            void CreatePlayerCharacter(int classIndex)
            {

                CharacterClass characterClass = (CharacterClass)classIndex;
                Console.WriteLine($"Player Class Choice: {characterClass}");
                PlayerCharacter = new Character(characterClass);
                PlayerCharacter.Health = 100;
                PlayerCharacter.BaseDamage = 20;
                PlayerCharacter.PlayerIndex = 0;

                CreateEnemyCharacter();
            }

            void CreateEnemyCharacter()
            {
                //randomly choose the enemy class and set up vital variables
                Random rand = new Random();
                int randomInteger = rand.Next(1, 4);
                CharacterClass enemyClass = (CharacterClass)randomInteger;
                Console.WriteLine($"Enemy Class Choice: {enemyClass}");
                EnemyCharacter = new Character(enemyClass);
                EnemyCharacter.Health = 100;
                PlayerCharacter.BaseDamage = 20;
                PlayerCharacter.PlayerIndex = 1;
                StartGame();
            }

            void StartGame()
            {
                //populates the character variables and targets
                EnemyCharacter.Target = PlayerCharacter;
                PlayerCharacter.Target = EnemyCharacter;
                AllPlayers.Add(PlayerCharacter);
                AllPlayers.Add(EnemyCharacter);
                AlocatePlayers();
                StartTurn();
            }

            void AlocatePlayers()
            {
                AlocatePlayerCharacter();
            }

            void AlocatePlayerCharacter()
            {
                int random = 0;
                GridCell RandomLocation = grid.cells.ElementAt(random);
                Console.Write($"{random}\n");
                if (!RandomLocation.occupied)
                {
                    GridCell PlayerCurrentLocation = RandomLocation;
                    RandomLocation.occupied = true;
                    grid.cells[random] = RandomLocation;
                    PlayerCharacter.currentCell = grid.cells[random];
                    AlocateEnemyCharacter();
                }
                else
                {
                    AlocatePlayerCharacter();
                }
            }

            void AlocateEnemyCharacter()
            {
                int random = 24;
                GridCell RandomLocation = grid.cells.ElementAt(random);
                Console.Write($"{random}\n");
                if (!RandomLocation.occupied)
                {
                    EnemyCurrentLocation = RandomLocation;
                    RandomLocation.occupied = true;
                    grid.cells[random] = RandomLocation;
                    EnemyCharacter.currentCell = grid.cells[random];
                    grid.DrawBattlefield(5, 5);
                }
                else
                {
                    AlocateEnemyCharacter();
                }
            }

            void StartTurn()
            {

                if (currentTurn == 0)
                {
                    //AllPlayers.Sort();  
                }

                foreach (Character character in AllPlayers)
                {
                    character.StartTurn(grid);
                }

                currentTurn++;
                HandleTurn();
            }

            void HandleTurn()
            {
                if (PlayerCharacter.Health == 0)
                {
                    return;
                }
                else if (EnemyCharacter.Health == 0)
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    // endgame?

                    Console.Write(Environment.NewLine + Environment.NewLine);

                    return;
                }
                else
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("Click on any key to start the next turn...\n");
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    ConsoleKeyInfo key = Console.ReadKey();
                    StartTurn();
                }
            }

            int GetRandomInt(int min, int max)
            {
                Random rand = new Random();
                int index = rand.Next(min, max);
                return index;
            }
        }
    }
}
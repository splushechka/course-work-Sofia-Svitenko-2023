﻿using System.Text.Json;
using boooooom.CommonTypes;
using boooooom.Entities;
using boooooom.Enums;
using boooooom.FileHelper;
using boooooom.Game;
using boooooom.JsonConverters;
using boooooom.Non_Entity_Classes;
using boooooom.Serializable;
using GameAction = boooooom.NonEntityClasses.GameAction;

namespace boooooom;

public class Program : BaseProgram
{
    private static List<List<string>> _levels = new()
    {
        new List<string>{"Resources/L1Layout.txt", "Resources/L1Data.json"},
        new List<string>{"Resources/L2Layout.txt","Resources/L2Data.json"},
        new List<string>{"Resources/L3Layout.txt", "Resources/L3Data.json"},
    };

    static async Task Main(string[] args)
    {
        Console.Clear();
        Console.CursorVisible = false;

        await ShowSplashScreenAsync();

        var musicTask = new Task(() => MusicPlayer.Play());
        musicTask.Start();

        while (Status != GameStatus.Finished)
        {
            Console.WriteLine("BomberCat 😸");

            Console.WriteLine("Press Space to start the Game!");

            Console.WriteLine("Press T to get instructions!");

            Console.WriteLine("Press R to close!");

            var key = Console.ReadKey();
            Console.WriteLine();

            if (key.Key == ConsoleKey.R)
            {
                Status = GameStatus.Finished;
                Thread.Sleep(1000);
                Console.Clear();
            }
            else if (key.Key == ConsoleKey.T)
            {
                ShowInstructions();
            }
            else if (key.Key == ConsoleKey.Spacebar)
            {
                Console.Clear();
                var currentLevel = 0;

                var levelKey = ConsoleKey.Spacebar;

                while (levelKey != ConsoleKey.R)
                {
                    Status = GameStatus.Active;
                    var result = GameLoop(_levels[currentLevel]);

                    Console.WriteLine("Select option");
                    if (result && currentLevel < _levels.Count - 1)
                    {
                        Console.WriteLine("Press Space to play next level");
                    }
                    Console.WriteLine("Press S to Replay this level");

                    if (currentLevel > 0)
                    {
                        Console.WriteLine("Press P to play previous level");
                    }
                    Console.WriteLine("Press R to exit");

                    levelKey = Console.ReadKey().Key;

                    if (levelKey == ConsoleKey.Spacebar)
                    {
                        if (result && currentLevel < _levels.Count - 1)
                        {
                            currentLevel += 1;
                        }
                    }
                    else if (levelKey == ConsoleKey.P)
                    {
                        currentLevel = currentLevel > 0 ? currentLevel - 1 : currentLevel;
                    }
                    else if (levelKey == ConsoleKey.S)
                    {
                        currentLevel = currentLevel;
                    }
                    else
                    {
                        levelKey = ConsoleKey.R;
                    }
                }
            }

            Console.Clear();
        }
    }

    private static async Task ShowSplashScreenAsync()
    {
        Console.Clear();
        Console.WriteLine("░░▄▄▄░░░░░░░░░░░░░░░░░░░░░░░░░░░░▄▄▄░░");
        Console.WriteLine("░▄████▄░░░░░░░░░░░░░░░░░░░░░░░▄▄████▄░");
        Console.WriteLine("░██░▀▀███▄▄░▄▄▄████████▄▄▄░▄▄███▀░███░");
        Console.WriteLine("░██░░░░░▀███████▀████▀▀██████▀░░░░███░");
        Console.WriteLine("░██▄░░░░░░░░░▀█▀░███░░░██▀▀░░░░░░░██▀░");
        Console.WriteLine("░▀██▄▄░░░░░░░░░░░░▀░░░░▀░░░░░░░▄▄▄██░░");
        Console.WriteLine("░░▀██▀░░░░░░░░░░░░░░░░░░░░░░░░░▀███▀░░");
        Console.WriteLine("░░▄██░░░░░░░░░░░░░░░░░░░░░░░░░░░░██▄░░");
        Console.WriteLine("░░████▀░░███░░░░░░░░░░░░░░███░░█████░░");
        Console.WriteLine("░░███▀░░░█████░░░░░░░░░░█████░░░▀███░░");
        Console.WriteLine("░░██░░░░░░▀▀▀▀░░░░░░░░░░▀▀▀▀░░░░░▀██░░");
        Console.WriteLine("▄▄███▄▄▄▄░░░░░░░░░░░░░░░░░░░░▄▄▄▄███▄▄");
        Console.WriteLine("░▄▄██▄▄░░░▄█░░░░▄▀▀▀▀▄░░░░█▄░░░▄███▄▄░");
        Console.WriteLine("▀░░▄████▀▀▀▀░░░░░▀▄▄▀░░░░░▀▀▀▀████▄░░▀");
        Console.WriteLine("░▄▀░░▀███▄▄░░░█▄▄█▀▀█▄▄▀░░░▄▄██▀░░░▀▄░");
        Console.WriteLine("░░░░░░░░▀███▄▄░░░░░░░░░░▄▄███▀░░░░░░░░");
        Console.WriteLine("░░░░░░░░░░▀▀████▄▄▄▄▄▄████▀▀░░░░░░░░░░");
        Console.WriteLine("░░░░░░░░░░░░░░▀▀▀▀▀▀▀▀▀▀░░░░░░░░░░░░░░");
        await Task.Delay(3000);
        Console.Clear();
    }

    private static void ShowInstructions()
    {
        Console.Clear();
        Console.WriteLine("Instructions:");
        Console.WriteLine("Our game is based on the famous game Bomberman and tells the story of a cat 😸 searching for fish 🐟, ");
        Console.WriteLine("while avoiding : 👾 (linear enemy moving along a line) and \U0001f9ff (chaotic enemy moving randomly).");
        Console.WriteLine("The cat uses bombs to destroy walls and enemies in the quest for fish.");
        Console.WriteLine("The cat needs to maintain its number of lives to avoid losing the game ❤️. Good luck!");
        Console.WriteLine();
        Console.WriteLine("Use W to move up");
        Console.WriteLine("Use S to move down");
        Console.WriteLine("Use A to move left");
        Console.WriteLine("Use D to move right");
        Console.WriteLine("Use Space to place a bomb");
        Console.WriteLine();
        Console.WriteLine("Press any key to return to the main menu");
        Console.ReadKey();
        Console.Clear();
    }

    private static GameAction ProcessKeyPress(ConsoleKeyInfo keyInfo)
    {
        GameAction action = null;

        switch (keyInfo.Key)
        {
            case ConsoleKey.W:
                action = new GameAction(ActionType.Move, new Coordinates(0, -1)); // Рух вгору
                break;
            case ConsoleKey.S:
                action = new GameAction(ActionType.Move, new Coordinates(0, 1)); // Рух вниз
                break;
            case ConsoleKey.A:
                action = new GameAction(ActionType.Move, new Coordinates(-1, 0)); // Рух вліво
                break;
            case ConsoleKey.D:
                action = new GameAction(ActionType.Move, new Coordinates(1, 0)); // Рух вправо
                break;
            case ConsoleKey.Spacebar:
                action = new GameAction(ActionType.PlaceBomb, new Coordinates(0, 0)); // Встановлення бомби
                break;
            default:
                break;
        }

        return action;
    }

    private static bool GameLoop(List<string> pathes)
    {
        string layoutPath = pathes[0];
        string levelDataPath = pathes[1];

        string jsonString = File.ReadAllText(levelDataPath);
        var options = new JsonSerializerOptions
        {
            Converters = { new EnemyListConverter() },
            WriteIndented = true
        };

        var levelSettings = JsonSerializer.Deserialize<LevelSettings>(jsonString, options);

        var playerCoords = new Coordinates(levelSettings.PlayerX, levelSettings.PlayerY);

        var levelInit = new LevelInitializer();
        var field = levelInit.ParseField(levelSettings.Height, levelSettings.Width, layoutPath);

        var render = new GameRender();
        var gameProcess = new GameProcess(playerCoords, levelSettings.Threshold, levelSettings.Enemies, render)
        {
            Field = field,
            PlayerCoords = playerCoords
        };

        var player = new PlayerEntity();

        var playerLives = player.Lives;

        Console.Clear();
        gameProcess.Render.Draw(field);
        gameProcess.Render.DrawScore(0, field.GetLength(0));
        gameProcess.Render.DrawLives(playerLives, field.GetLength(0));

        while (!gameProcess.IsGameOver)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                var action = ProcessKeyPress(keyInfo);

                gameProcess.Action = action;
            }
        }

        gameProcess.Timer.StopTimer();
        Thread.Sleep(100);
        Status = GameStatus.Paused;
        Console.Clear();
        Console.WriteLine("Game over");

        return gameProcess.Win;
    }
}

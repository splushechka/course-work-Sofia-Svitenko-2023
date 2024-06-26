using boooooom.CommonTypes;
using boooooom.Entities;
using boooooom.Enums;
using boooooom.FileHelper;
using boooooom.Game;
using boooooom.JsonConverters;
using boooooom.Non_Entity_Classes;
using boooooom.NonEntityClasses;
using boooooom.Serializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WinFormsApp;
using WinFormsApp.UIHelpers;

namespace WinFormsApp.UI
{
    public class ProgramUI : BaseProgram
    {
        public static List<List<string>> _levels = new()
        {
            new List<string>{"Resources/L1Layout.txt", "Resources/L1Data.json"},
            new List<string>{"Resources/L2Layout.txt","Resources/L2Data.json"},
            new List<string>{"Resources/L3Layout.txt", "Resources/L3Data.json"},
        };

        public static int CurrentLevel { get; set; }

        public static GameProcess? CurrentGame { get; set; }

        public static void StartGame(BomberCat form, UIRender render)
        {
            var musicTask = new Task(() => MusicPlayer.Play());
            musicTask.Start();

            Status = GameStatus.Active;
            var result = GameLoop(_levels[CurrentLevel], form, render);
            Status = GameStatus.Finished;
        }

        private static bool GameLoop(List<string> pathes, BomberCat form, UIRender render)
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

            render.DrawLives(3, 0);
            render.DrawScore(3, 0);

            var gameProcess = new GameProcess(playerCoords, levelSettings.Threshold, levelSettings.Enemies, render)
            {
                Field = field,
                PlayerCoords = playerCoords
            };

            gameProcess.Render.Draw(gameProcess.Field);

            CurrentGame = gameProcess;

            while (!gameProcess.IsGameOver && Status == GameStatus.Active)
            {
                Application.DoEvents(); // Process all Windows messages currently in the message queue

                if (render.CurrentAction != null)
                {
                    gameProcess.Action = render.CurrentAction;
                    render.CurrentAction = null; // Reset the action after processing
                }
            }

            return gameProcess.Win;
        }
    }
}

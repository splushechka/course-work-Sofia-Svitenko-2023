﻿using boooooom.Cells;
using boooooom.Interfaces;
using boooooom.Non_Entity_Classes;

namespace boooooom;

public class GameRender : IRender
{
    public void Draw(Cell[,] Field)
    {
        for (int i = 0; i < Field.GetLength(0); i++)
        {
            for (int j = 0; j < Field.GetLength(1); j++)
            {
                var cell = Field[i, j];
                var symbol = cell.GetDrawSymbol();
                Console.Write(symbol);

            }
            Console.WriteLine();
        }
    }
    
    public void DrawScore(int Score, int fieldHeight)
    {
        Console.SetCursorPosition(0, fieldHeight);
        Console.WriteLine("Score:" + Score);
    }
    
    public void DrawLives(int lives, int fieldHeight)
    {
        string hearts = "❤️";
        string livesString = string.Join("", Enumerable.Repeat(hearts, lives));

        Console.SetCursorPosition(0, fieldHeight + 1);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, fieldHeight + 1);
        Console.WriteLine("Lives: " + livesString);
    }
    
    public void DrawChanges(List<(Coordinates coords, Cell cell)> changedCells)
    {
        // Вивести лише змінені клітини
        foreach (var (coords, cell) in changedCells)
        {
            Console.SetCursorPosition(coords.X * 2, coords.Y);
            var symbol = cell.GetDrawSymbol();
            Console.Write(symbol);
        }
    }
    public void ClearField()
    {
        Console.Clear();
    }
}

using boooooom.Cells;
using boooooom.Game;
using boooooom.Non_Entity_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boooooom.Interfaces
{
    public interface IRender
    {
        public void Draw(Cell[,] Field);

        public void DrawScore(int Score, int fieldHeight);

        public void DrawLives(int lives, int fieldHeight);

        public void DrawChanges(List<(Coordinates coords, Cell cell)> changedCells);

        public void ClearField();
    }
}

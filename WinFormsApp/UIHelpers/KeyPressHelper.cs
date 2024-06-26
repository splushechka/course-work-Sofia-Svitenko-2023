using boooooom.Enums;
using boooooom.Non_Entity_Classes;
using boooooom.NonEntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.UIHelpers
{
    internal class KeyPressHelper
    {
        internal static void HandleKeyPress(KeyEventArgs key)
        {
            GameAction action = null;

            switch (key.KeyCode)
            {
                case Keys.W:
                    action = new GameAction(ActionType.Move, new Coordinates(0, -1)); // Рух вгору
                    break;
                case Keys.S:
                    action = new GameAction(ActionType.Move, new Coordinates(0, 1)); // Рух вниз
                    break;
                case Keys.D:
                    action = new GameAction(ActionType.Move, new Coordinates(1, 0)); // Рух вправо
                    break;
                case Keys.A:
                    action = new GameAction(ActionType.Move, new Coordinates(-1, 0)); // Рух вліво
                    break;
                case Keys.Space:
                    action = new GameAction(ActionType.PlaceBomb, new Coordinates(0, 0)); // Встановлення бомби
                    break;
            }
        }
    }
}

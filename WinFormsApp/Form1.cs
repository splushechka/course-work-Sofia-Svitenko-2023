using boooooom.Enums;
using System.Windows.Forms;
using WinFormsApp.UI;
using WinFormsApp.UIHelpers;

namespace WinFormsApp
{
    public partial class BomberCat : Form
    {
        private UIRender _render;
        public BomberCat()
        {
            InitializeComponent();
            _render = new UIRender(this);
            _render.GenerateLabels(this);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (ProgramUI.Status != GameStatus.Active)
            {
                ProgramUI.StartGame(this, _render);
            }
        }

        private void InstructionButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(("Instructions: Our game is based on the famous game Bomberman and tells the story of a cat 😸 searching for fish 🐟, while avoiding : 👾 (linear enemy moving along a line) and \U0001f9ff (chaotic enemy moving randomly).The cat uses bombs to destroy walls and enemies in the quest for fish.The cat needs to maintain its number of lives to avoid losing the game ❤️. Good luck!"));
        }

        private void NextLevelButton_Click(object sender, EventArgs e)
        {
            if (ProgramUI.CurrentLevel < ProgramUI._levels.Count - 1)
            {
                ProgramUI.CurrentLevel++;
                _render.DrawLevels();
            }
        }

        private void PrevLevelButton_Click(object sender, EventArgs e)
        {
            if (ProgramUI.CurrentLevel > 0)
            {
                ProgramUI.CurrentLevel--;
                _render.DrawLevels();
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProgramUI.Status = GameStatus.Finished;
        }
    }
}

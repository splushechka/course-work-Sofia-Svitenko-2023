using boooooom.Cells;
using boooooom.Enums;
using boooooom.Game;
using boooooom.Interfaces;
using boooooom.Non_Entity_Classes;
using boooooom.NonEntityClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using WinFormsApp.UI;

namespace WinFormsApp.UIHelpers
{
    public class UIRender : IRender
    {
        private TableLayoutPanel _tableLayoutPanel;

        private Panel _panel;

        private readonly Dictionary<string, Bitmap> _spriteDictionary;

        private BomberCat _form;

        private Label _levelLabel;

        private Label _scoreLabel;

        private Label _livesLabel;

        public GameAction CurrentAction { get; set; } // Shared variable for storing the current action


        public UIRender(BomberCat form)
        {
            _form = form;
            _spriteDictionary = LoadSprites();
            _form.KeyDown += HandleKeyPress; // Subscribe to the KeyDown event
            _form.KeyPreview = true; // Ensure the form receives key events
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    CurrentAction = new GameAction(ActionType.Move, new Coordinates(0, -1)); // Move up
                    break;
                case Keys.S:
                    CurrentAction = new GameAction(ActionType.Move, new Coordinates(0, 1)); // Move down
                    break;
                case Keys.D:
                    CurrentAction = new GameAction(ActionType.Move, new Coordinates(1, 0)); // Move right
                    break;
                case Keys.A:
                    CurrentAction = new GameAction(ActionType.Move, new Coordinates(-1, 0)); // Move left
                    break;
                case Keys.E:
                    CurrentAction = new GameAction(ActionType.PlaceBomb, new Coordinates(0, 0)); // Place bomb
                    break;
                default:
                    CurrentAction = null;
                    break;
            }
        }

        public void Draw(Cell[,] field)
        {
            const int containerSize = 900;
            int cellSize = containerSize / Math.Max(field.GetLength(0), field.GetLength(1));

            // Create a Panel to contain the TableLayoutPanel
            _panel = new Panel
            {
                Dock = DockStyle.None,
                Location = new Point(550, 10),
                AutoScroll = true, // Enable scrolling if content is larger than the container
                Size = new Size(field.GetLength(1) * cellSize + 20, field.GetLength(0) * cellSize + 20) // Add some padding for scrollbars

            };

            _tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.None,
                RowCount = field.GetLength(0),
                ColumnCount = field.GetLength(1),
                Location = new Point(0, 0),
                Size = new Size(field.GetLength(1) * cellSize, field.GetLength(0) * cellSize)
            };

            for (int i = 0; i < field.GetLength(0); i++)
            {
                _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, cellSize));
            }

            for (int j = 0; j < field.GetLength(1); j++)
            {
                _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, cellSize));
            }

            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    var pictureBox = new PictureBox
                    {
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Dock = DockStyle.Fill,
                        BackgroundImageLayout = ImageLayout.Zoom,
                        BackgroundImage = Properties.Resources.EmptyCell,
                        Image = _spriteDictionary[field[row, col].GetDrawImage()]
                    };
                    _tableLayoutPanel.Controls.Add(pictureBox, col, row);
                }
            }

            // Add the TableLayoutPanel to the Panel container
            _panel.Controls.Add(_tableLayoutPanel);

            // Add the Panel container to the form
            _form.Controls.Add(_panel);
        }

        public void GenerateLabels(BomberCat form)
        {
            // Create labels
            _levelLabel = new Label();
            _scoreLabel = new Label();
            _livesLabel = new Label();

            _levelLabel.AutoSize = true;
            _levelLabel.Font = new Font("Book Antiqua", 18F);
            _levelLabel.Location = new Point(1600, 187);
            _levelLabel.Name = "LevelLabel";
            _levelLabel.Size = new Size(191, 37);
            _levelLabel.TabIndex = 5;
            _levelLabel.Text = "Game Level: 0 ";
            // 
            // ScoreLabel
            // 
            _scoreLabel.AutoSize = true;
            _scoreLabel.Font = new Font("Book Antiqua", 18F);
            _scoreLabel.Location = new Point(1600, 107);
            _scoreLabel.Name = "ScoreLabel";
            _scoreLabel.Size = new Size(197, 37);
            _scoreLabel.TabIndex = 6;
            _scoreLabel.Text = "Player Score: 0 ";
            // 
            // LivesLabel
            // 
            _livesLabel.AutoSize = true;
            _livesLabel.Font = new Font("Book Antiqua", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            _livesLabel.Location = new Point(1600, 29);
            _livesLabel.Name = "LivesLabel";
            _livesLabel.Size = new Size(197, 37);
            _livesLabel.TabIndex = 7;
            _livesLabel.Text = "Player Lives: - ";

            // Add labels to the form
            form.Controls.Add(_levelLabel);
            form.Controls.Add(_scoreLabel);
            form.Controls.Add(_livesLabel);
        }

        private Dictionary<string, Bitmap> LoadSprites()
        {
            return new Dictionary<string, Bitmap>
            {
                { "EmptyCell", Properties.Resources.EmptyCell },
                { "BrickWall", Properties.Resources.BrickWall },
                { "StoneWall", Properties.Resources.StoneWall },
                { "Bomb", Properties.Resources.Bomb },
                { "Prize", Properties.Resources.Prize },
                { "Heart", Properties.Resources.Heart },
                { "LinearEnemy", Properties.Resources.LinearEnemy },
                { "ChaoticEnemy", Properties.Resources.ChaoticEnemy },
                { "Explosion", Properties.Resources.Explosion },
                { "Player", Properties.Resources.Player },
                { "SadPlayer", Properties.Resources.SadPlayer },
                { "ScaredPlayer", Properties.Resources.ScaredPlayer },
                { "HappyPlayer", Properties.Resources.HappyPlayer },
            };
        }

        public void DrawScore(int score, int fieldHeight)
        {
            var value = _scoreLabel.Text.Substring(0, 13) + score.ToString();
            if (_scoreLabel.InvokeRequired)
            {
                _scoreLabel.BeginInvoke((MethodInvoker)(() =>
                {
                    _scoreLabel.Text = value;
                }));
            }
            else
            {
                _scoreLabel.Text = value;
            }
        }

        public void DrawLives(int lives, int fieldHeight)
        {
            var value = _livesLabel.Text.Substring(0, 13) + lives.ToString();
            if (_livesLabel.InvokeRequired)
            {
                _livesLabel.Invoke((MethodInvoker)(() => {
                    _livesLabel.Text = value;
                }));
            }
            else
            {
                _livesLabel.Text = value;
            }
        }
        public void DrawLevels()
        {
            var nextLevel = ProgramUI.CurrentLevel + 1;
            var value = _levelLabel.Text.Substring(0, 12) + nextLevel.ToString();

            if (_levelLabel.InvokeRequired)
            {
                _levelLabel.Invoke((MethodInvoker)(() => {
                    _levelLabel.Text = value;
                }));
            }
            else
            {
                _levelLabel.Text = value;
            }
        }

        public void DrawChanges(List<(Coordinates coords, Cell cell)> changedCells)
        {
            foreach (var (coords, cell) in changedCells)
            {
                var pictureBox = (PictureBox)_tableLayoutPanel.GetControlFromPosition(coords.X, coords.Y);
                if (pictureBox != null)
                {
                    pictureBox.Image = _spriteDictionary[cell.GetDrawImage()];
                }
            }
        }

        public void ClearField()
        {
            if (_form.InvokeRequired)
            {
                _form.Invoke((MethodInvoker)(() =>
                {
                    _form.Controls.Remove(_panel);
                }));
            }
            else
            {
                _form.Controls.Remove(_panel);
            }
        }
    }
}
namespace WinFormsApp
{
    partial class BomberCat
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BomberCat));
            startButton = new Button();
            instructionButton = new Button();
            nextLevelButton = new Button();
            prevLevelButton = new Button();
            exitButton = new Button();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.Location = new Point(78, 125);
            startButton.Margin = new Padding(4);
            startButton.Name = "startButton";
            startButton.Size = new Size(399, 88);
            startButton.TabIndex = 0;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += StartButton_Click;
            // 
            // instructionButton
            // 
            instructionButton.Location = new Point(78, 268);
            instructionButton.Margin = new Padding(4);
            instructionButton.Name = "instructionButton";
            instructionButton.Size = new Size(399, 88);
            instructionButton.TabIndex = 1;
            instructionButton.Text = "Instructions";
            instructionButton.UseVisualStyleBackColor = true;
            instructionButton.Click += InstructionButton_Click;
            // 
            // nextLevelButton
            // 
            nextLevelButton.Location = new Point(78, 406);
            nextLevelButton.Margin = new Padding(4);
            nextLevelButton.Name = "nextLevelButton";
            nextLevelButton.Size = new Size(399, 88);
            nextLevelButton.TabIndex = 2;
            nextLevelButton.Text = "Next Level";
            nextLevelButton.UseVisualStyleBackColor = true;
            nextLevelButton.Click += NextLevelButton_Click;
            // 
            // prevLevelButton
            // 
            prevLevelButton.Location = new Point(78, 547);
            prevLevelButton.Margin = new Padding(4);
            prevLevelButton.Name = "prevLevelButton";
            prevLevelButton.Size = new Size(399, 88);
            prevLevelButton.TabIndex = 3;
            prevLevelButton.Text = "Previous Level";
            prevLevelButton.UseVisualStyleBackColor = true;
            prevLevelButton.Click += PrevLevelButton_Click;
            // 
            // exitButton
            // 
            exitButton.Location = new Point(78, 681);
            exitButton.Margin = new Padding(4);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(399, 88);
            exitButton.TabIndex = 4;
            exitButton.Text = "Exit";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += ExitButton_Click;
            // 
            // BomberCat
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PaleTurquoise;
            ClientSize = new Size(1898, 1024);
            Controls.Add(exitButton);
            Controls.Add(prevLevelButton);
            Controls.Add(nextLevelButton);
            Controls.Add(instructionButton);
            Controls.Add(startButton);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "BomberCat";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BomberCat";
            FormClosed += Form1_FormClosed;
            ResumeLayout(false);
        }

        #endregion

        private Button startButton;
        private Button instructionButton;
        private Button nextLevelButton;
        private Button prevLevelButton;
        private Button exitButton;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CS_GridGame_Team5
{
    public partial class gameOver : Form
    {
        // Some Variables
        public string winner = "???";
        public int score = 0;
        public gameOver()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.gameOver_Load);
        }

        private void gameOver_Load(object sender, EventArgs e)
        {
            textBox2.Text = winner + "wins!!!\n\rScore : " + score.ToString() + "\n\rEnter your name below and click Menu or Replay to save your score.";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void replayButton_Click(object sender, EventArgs e)
        {
            saveScore();
            this.Hide();
            Form_Game gameWindow = new Form_Game();
            gameWindow.ShowDialog();
        }

        /**
         * Fuction to save Score
         */
        private void saveScore()
        {
            FileInfo f = new FileInfo("../../Assets/highScores.txt"); // Set file name
            StreamWriter w = f.CreateText(); // Create file
            w.WriteLine(playerName.Text + "," + score);
            w.Close();
        }

        private void mainMenuButton_Click(object sender, EventArgs e)
        {
            saveScore();
            this.Hide();
            Form2 gameWindow = new Form2();
            gameWindow.ShowDialog();
        }

        /**
         * Function to be called once the game has been won
         * */
        public void gameWon(WinCondition condition, int score)
        {   
            winner = condition.ToString();
            this.score = score;
        }
    }
}

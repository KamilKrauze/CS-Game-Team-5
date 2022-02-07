﻿using System;
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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        /**
        public class Score
        {
            // Feilds
            private string name;
            private int score;

            //Constructor
            Score(string newName, int newScore)
            {
                name = newName;
                score = newScore;
            }

            // getter method for name
            public string getName()
            {
                return name;
            }

            // getter method for score
            public int getScore()
            {
                return score;
            }
        }
        */

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            List<string> rawHighScores = new List<string>(); // Creats a list for highscores being read from high scores file
            
            // Gets contents for highScores file
            StreamReader s = File.OpenText(@"highScores.txt"); // Set file name
            string read = null;
            while ((read = s.ReadLine()) != null) // Read from file until done
            {
                rawHighScores.Add(read); // Output text to highscores list
            }
            s.Close();

            List<String[]> highScores = new List<String[]>(); // Makes new list for High scores once they have been processed
            int counter = 0;
            while (rawHighScores[counter] != null) // While there are rawHighScores, split score from player name for hisg scores
            {
                highScores.Add(rawHighScores[counter].Split(','));
                counter ++;
            }

            highScores = sort(highScores); // sort high scores

            // Display first 10 high scores
            String textBox = "";
            for (int i = 0; i < 10; i++)
            {
                textBox = textBox + "\n\r1\t" + highScores[i][1] + "\t" + highScores[i][0];
            }
        }
        /**
         * Method to sort a list, specifically highscores
         **/
        public List<String[]> sort(List<String[]> highScores)
        {
            int counter = 0;
            String[] tempScore;
            while (highScores[counter] != null)
            {
                if (highScores[counter + 1] == null)
                {
                    break;
                }
                else
                {
                    if (Int32.Parse(highScores[counter][1]) < Int32.Parse(highScores[counter + 1][1]))
                    {
                        tempScore = highScores[counter];
                        highScores[counter] = highScores[counter + 1];
                        highScores[counter + 1] = tempScore;
                        counter ++;
                    }
                }
            }
            return highScores;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 mainMenuWindow = new Form2();
            mainMenuWindow.ShowDialog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject_Game_
{
    public partial class Game_Form : Form
    {
        private WarGame game;
        public Game_Form()
        {
            InitializeComponent();
        }
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            game = new WarGame();
            UpdateUI(null, null, "Game started!");
            button1.Hide();
        }

        private void btnPlayRound_Click(object sender, EventArgs e)
        {
            var (player1Card, player2Card, result) = game.PlayRound();
            UpdateUI(player1Card, player2Card, result);
        }

        private void UpdateUI(Card player1Card, Card player2Card, string result)
        {
            if (player1Card != null)
            {
                label1.Text = player1Card.ToString();
            }
            else
            {
                label1.Text = "No card";
            }
            //----------------------
            if (player2Card != null)
            {
                label2.Text = player2Card.ToString();
            }
            else
            {
                label2.Text = "No card";
            }
            //-------------------------
            label3.Text = result;
            label4.Text = $"Player 1 Deck: {game.GetPlayer1DeckCount()} cards";
            label5.Text = $"Player 2 Deck: {game.GetPlayer2DeckCount()} cards";
        }

        private void btn_Click_Exit(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace theGameOf99v2
{
    public partial class PlayerSetup : Form
    {
        public PlayerSetup() //ctor doesn't do anything neat here
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) //2 or 3 players, not too hard
        {
            var player3Enabled = checkBox1.Checked;
            player3name.Enabled = player3Enabled;
            switch (checkBox1.Checked)
            {
                case true:
                    player3name.Enabled = true;
                    label4.ForeColor = Color.Green;
                    break;
                case false:
                    player3name.Enabled = false;
                    label4.ForeColor = Color.Gray;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e) //let's play a Game why not
        {
            string p1name = player1name.Text;
            string p2name = player2name.Text;
            string p3name = player3name.Text;
            Player[] players;

            if (p1name.Trim() == "")
            {
                p1name = "Player 1";               
            }
            if (p2name.Trim() == "")
            {
                p2name = "Player 2";
            }
            if (p3name.Trim() == "")
            {
                p3name = "Player 3";
            }
            var player1 = new Player(p1name, 1);
            var player2 = new Player(p2name, 2);
            if (checkBox1.Checked)
            {
                Player player3 = new Player(p3name, 3);
                players = new Player[] { player1, player2, player3 };
            }
            else
            {
                players = new Player[] { player1, player2 };
            }
            var LeJeuDe99 = new Game(players);
            this.Hide();
        }
    }
}

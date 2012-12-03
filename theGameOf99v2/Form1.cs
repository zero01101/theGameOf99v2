using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace theGameOf99v2
{
    public partial class Form1 : Form
    {
        public Form1() //ctor doesn't do anything neat here
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) //2 or 3 players, not too hard
        {
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

        private void button1_Click(object sender, EventArgs e) //let's play a game why not
        {
            string p1name = player1name.Text;
            string p2name = player2name.Text;
            string p3name = player3name.Text;
            player[] players;

            if (p1name.Trim() == "")
            {
                p1name = "player 1";               
            }
            if (p2name.Trim() == "")
            {
                p2name = "player 2";
            }
            if (p3name.Trim() == "")
            {
                p3name = "player 3";
            }
            player player1 = new player(p1name, 1);
            player player2 = new player(p2name, 2);
            if (checkBox1.Checked)
            {
                player player3 = new player(p3name, 3);
                players = new player[] { player1, player2, player3 };
            }
            else
            {
                players = new player[] { player1, player2 };
            }
            game LeJeuDe99 = new game(players);
            this.Hide();
        }
    }
}

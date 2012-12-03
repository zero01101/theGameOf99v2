using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace theGameOf99v2
{
    public partial class playarea : Form
    {
        public int[][] rows;
        public game currentGame;

        public playarea(game gameInProgress) //ctor - happens right when you instantiate the class - this stuff should be in every new game, but only once per game or something
        {
            currentGame = gameInProgress;
            InitializeComponent();
            //board layout arrays
            int[] row1 = new int[10]  { 73, 72, 71, 70, 69, 68, 67, 66, 65, 100};
            int[] row2 = new int[10]  { 74, 57, 58, 59, 60, 61, 62, 63, 64, 99 };
            int[] row3 = new int[10]  { 75, 56, 21, 20, 19, 18, 17, 36, 37, 98 };
            int[] row4 = new int[10]  { 76, 55, 22, 13, 14, 15, 16, 35, 38, 97 };
            int[] row5 = new int[10]  { 77, 54, 23, 12,  1,  4,  5, 34, 39, 96 };
            int[] row6 = new int[10]  { 78, 53, 24, 11,  2,  3,  6, 33, 40, 95 };
            int[] row7 = new int[10]  { 79, 52, 25, 10,  9,  8,  7, 32, 41, 94 };
            int[] row8 = new int[10]  { 80, 51, 26, 27, 28, 29, 30, 31, 42, 93 };
            int[] row9 = new int[10]  { 81, 50, 49, 48, 47, 46, 45, 44, 43, 92 };
            int[] row10 = new int[10] { 82, 83, 84, 85, 86, 87, 88, 89, 90, 91 };

            rows = new int[][] { row1, row2, row3, row4, row5, row6, row7, row8, row9, row10 };

            for (int i = 0; i < rows.Length; i++) //row
            {
                int[] row = rows[i];

                for (int j = 0; j < row.Length; j++) //column
                {
                    //lets make a play area howabout, 10x10 and arranged in that bizarre matrix above
                    int s = 35; //arbitrary woo
                    boardsquare square = new boardsquare(); //boardsquare object inherits button - see boardsquare.cs
                    square.Name = "bsq" + row[j];
                    square.Text = row[j].ToString();
                    square.squareID = row[j];
                    square.row = i;
                    square.column = j;
                    square.Size = new System.Drawing.Size(s, s);
                    square.Location = new Point(j * s, i * s);
                    square.Click += new EventHandler(square_Click);
                    square.Enabled = false;
                    this.Controls.Add(square);
                }
            }
            //oops, this is the game of 99, not 100            
            this.Controls.RemoveByKey("bsq100");            
        } 

        public void roundUpdate(player nextPlayer) //this progresses the visual elements on the form to the next player and should be called after a player's play choice is confirmed
        {
            if (nextPlayer.hand.Count > 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    RadioButton cardbutton = (this.Controls["panel1"].Controls["radioCard" + i] as RadioButton);
                    cardbutton.Checked = false;
                    cardbutton.Visible = false;
                }
                for (int i = 1; i < 100; i++)
                {
                    boardsquare square = (this.Controls["bsq" + i] as boardsquare);
                    square.Enabled = false;
                }
                lblPlayer.Text = nextPlayer.name;
                for (int i = 0; i < nextPlayer.hand.Count; i++)
                {
                    RadioButton cardbutton = (this.Controls["panel1"].Controls["radioCard" + i] as RadioButton);
                    cardbutton.Visible = true;
                    cardbutton.Text = nextPlayer.hand[i].ToString() + " to 99";
                }
                if (panel1.Visible)
                {
                    panel1.Visible = false;
                    btnHand.Text = "show hand";
                }
                btnDrawCard.Enabled = false;
                btnDiscard.Enabled = false;
                if (nextPlayer.hand.Count < 5)
                {
                    btnDrawCard.Enabled = true;
                }
            }
            else
            {
                currentGame.prepareNextPlayer();
                roundUpdate(currentGame.currentPlayer);
            }
        }

        void square_Click(object sender, EventArgs e) //wut happen what you click a squrr
        {
            var square = (sender as boardsquare);
            if (!(square.occupied))
            {
                square.occupy(square, currentGame.currentPlayer, currentGame.currentPlayer.selectedCard);
                if (square.occupied)
                {
                    //currentGame.checkForWin(square);
                    bool gameover = currentGame.checkForWin();
                    if (gameover == false)
                    {
                        currentGame.prepareNextPlayer();
                        roundUpdate(currentGame.currentPlayer); //technically NEXT player gets implemented because player.ID == players[indexer++]                    
                    }
                    else
                    { Application.Exit(); }
                }
            }
        }

        void card_checked(object sender, EventArgs e) //radiobox clicked - next, update the clickables
        {            
            var rad = (sender as RadioButton);            
            int i = int.Parse(rad.Text.ToString().Replace(" to 99", ""));
            currentGame.currentPlayer.selectedCard = i;
            updateAvailableSquares(i);                                                
            btnDiscard.Enabled = true;
        }

        void updateAvailableSquares(int chosenCard) //this updates the boardsquare clickables
        {
            for (int i = 1; i < chosenCard; i++)
            {
                boardsquare b = (this.Controls["bsq" + i.ToString()] as boardsquare);
                b.Enabled = false;
            }
            for (int i = chosenCard; i < 100; i++)
            {
                boardsquare b = (this.Controls["bsq" + i.ToString()] as boardsquare);
                if (!(b.occupied)) { b.Enabled = true; }
            }
        }

        private void btnHand_Click(object sender, EventArgs e) //change the state of the panel with the card radiobuttons
        {
            switch (panel1.Visible)
            {
                case true:
                    panel1.Visible = false;
                    btnHand.Text = "show hand";
                    btnDrawCard.Enabled = false;
                    break;
                case false:
                    panel1.Visible = true;
                    btnHand.Text = "hide hand";
                    if (currentGame.currentPlayer.hand.Count < 5)
                    {
                        btnDrawCard.Enabled = true;
                    }
                    break;
            }
        }

        private void playarea_FormClosed(object sender, FormClosedEventArgs e) //gbye
        {
            Application.Exit();
        }

        private void btnDiscard_Click(object sender, EventArgs e) //get ridda thet there card there
        {            
            DialogResult result = MessageBox.Show("are you sure you wish to discard " + currentGame.currentPlayer.selectedCard.ToString() + "?","HALT",MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                currentGame.currentPlayer.removeCard(currentGame.currentPlayer, currentGame.currentPlayer.selectedCard);
                currentGame.currentPlayer.hand.Sort();
                currentGame.prepareNextPlayer();
                roundUpdate(currentGame.currentPlayer); //technically NEXT player
            }
        }

        private void btnDrawCard_Click(object sender, EventArgs e) //MOAR CARDS PLS
        {
            DialogResult result = MessageBox.Show("are you sure you wish to draw a new card?", "HALT", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {                
                currentGame.currentPlayer.hand.Add(currentGame.gameDeck.draw(currentGame.gameDeck));
                currentGame.currentPlayer.hand.Sort();
                currentGame.prepareNextPlayer();
                roundUpdate(currentGame.currentPlayer); //technically NEXT player
            }
        }
    }
}

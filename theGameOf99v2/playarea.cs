using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace theGameOf99v2
{
    
    public partial class playarea : Form
    {
        public game currentGame;
        private IList<ISquareCollection> _squares;

        public playarea(game gameInProgress) //ctor - happens right when you instantiate the class - this stuff should be in every new game, but only once per game or something
        {
            _squares = new List<ISquareCollection>();
            currentGame = gameInProgress;
            InitializeComponent();
            _squares = BoardBuilder.BuildBoard(square => 
            {
                square.Click += square_Click;
                Controls.Add(square);
            });
        }

        public bool CheckifWinner(player player)
        {
            return _squares.Any(x => x.CheckIfWinner(player));
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
                foreach (var square in this.Controls.OfType<boardsquare>())
                {
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

        private void square_Click(object sender, EventArgs e) //wut happen what you click a squrr
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
                boardsquare b = Controls.GetSquare(i);
                b.Enabled = false;
            }
            for (int i = chosenCard; i < 100; i++)
            {
                boardsquare b = this.Controls.GetSquare(i);
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

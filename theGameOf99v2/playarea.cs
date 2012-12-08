using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using theGameOf99v2.Interfaces;

namespace theGameOf99v2
{
    public partial class PlayArea : Form
    {
        private readonly IList<ISquareCollection> _columns;

        public PlayArea(Game gameInProgress)
            //ctor - happens right when you instantiate the class - this stuff should be in every new Game, but only once per Game or something
        {
            _columns = new List<ISquareCollection>();
            CurrentGame = gameInProgress;
            InitializeComponent();
            _columns = BoardBuilder.BuildBoard(square =>
            {
                square.Click += OnSquareClick;
                Controls.Add(square);
            });
        }

        public Game CurrentGame { get; private set; }

        public bool CheckifWinner(Player player)
        {
            return _columns.Any(x => x.CheckIfWinner(player));
        }

        public void RoundUpdate(Player nextPlayer)
            //this progresses the visual elements on the form to the next Player and should be called after a Player's play choice is confirmed
        {
            if (nextPlayer.Hand.Count > 0)
            {
                var cardPanelControls = Controls["cardsPanel"].Controls;
                cardPanelControls.OfType<RadioButton>()
                                      .Each(rb =>
                                      {
                                          rb.Checked = false;
                                          rb.Visible = false;
                                      });

                Controls.OfType<BoardSquare>().Each(bs => bs.Enabled = false);

                lblPlayer.Text = nextPlayer.Name;

                cardPanelControls.GetItems<Card>(0, nextPlayer.Hand.Count)
                                      .Each((card, index) => 
                                      {
                                          card.Visible = true;
                                          card.Text = nextPlayer.Hand[index] + " to 99";
                                      });

                if (cardsPanel.Visible)
                {
                    cardsPanel.Visible = false;
                    btnHand.Text = "show Hand";
                }

                btnDrawCard.Enabled = false;
                btnDiscard.Enabled = false;
                if (nextPlayer.Hand.Count < 5)
                {
                    btnDrawCard.Enabled = true;
                }
            }
            else
            {
                CurrentGame.PrepareNextPlayer();
                RoundUpdate(CurrentGame.CurrentPlayer);
            }
        }

        private void OnCardChecked(object sender, EventArgs e)
            //radiobox clicked - next, update the clickables
        {
            var rad = (RadioButton)sender;
            int card = int.Parse(rad.Text.Replace(" to 99", String.Empty));
            CurrentGame.CurrentPlayer.SelectedCard = card;
            UpdateAvailableSquares(card);
            btnDiscard.Enabled = true;
        }

        private void OnDiscardClicked(object sender, EventArgs e) //get ridda thet there card there
        {
            if (DialogResult.Yes
                == MessageBox.Show(
                    "are you sure you wish to discard " + CurrentGame.CurrentPlayer.SelectedCard.ToString()
                    + "?",
                    "HALT",
                    MessageBoxButtons.YesNo))
            {
                CurrentGame.CurrentPlayer.RemoveCardSelectedCard();
                CurrentGame.CurrentPlayer.Hand.Sort();
                CurrentGame.PrepareNextPlayer();
                RoundUpdate(CurrentGame.CurrentPlayer); //technically NEXT Player
            }
        }

        private void OnDrawCardClicked(object sender, EventArgs e) //MOAR CARDS PLS
        {
            if (DialogResult.Yes
                == MessageBox.Show("are you sure you wish to draw a new card?",
                    "HALT",
                    MessageBoxButtons.YesNo))
            {
                CurrentGame.CurrentPlayer.Hand.Add(CurrentGame.Deck.DrawCard());
                CurrentGame.CurrentPlayer.Hand.Sort();
                CurrentGame.PrepareNextPlayer();
                RoundUpdate(CurrentGame.CurrentPlayer); //technically NEXT Player
            }
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e) //gbye
        {
            Application.Exit();
        }

        private void OnSquareClick(object sender, EventArgs e) //wut happen what you click a squrr
        {
            var square = (sender as BoardSquare);
            if (!(square.Occupied))
            {
                square.Occupy(CurrentGame.CurrentPlayer);
                if (square.Occupied)
                {
                    //currentGame.CheckForWin(square);
                    bool gameover = CurrentGame.CheckForWin();
                    if (gameover == false)
                    {
                        CurrentGame.PrepareNextPlayer();
                        RoundUpdate(CurrentGame.CurrentPlayer);
                            //technically NEXT Player gets implemented because Player.ID == players[indexer++]                    
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
        }

        private void OnToggleHandClicked(object sender, EventArgs e)
            //change the state of the panel with the card radiobuttons
        {
            bool isVisible = cardsPanel.Visible;
            btnHand.Text = isVisible ? "Show Hand" : "Hide Hand";
            btnDrawCard.Enabled = !isVisible;
            cardsPanel.Visible = !isVisible;
        }

        private void UpdateAvailableSquares(int chosenCard) //this updates the BoardSquare clickables
        {
            //Disable all the cards under the chosen card
            Controls.GetItems<BoardSquare>(1, chosenCard).Each(s => s.Enabled = false);

            //Enable all of the non-occupied cards above it.
            Controls.GetItems<BoardSquare>(chosenCard, 100).Where(x => !x.Occupied).Each(x => x.Enabled = true);
        }
    }
}
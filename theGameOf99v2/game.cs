using System;
using System.Windows.Forms;

namespace theGameOf99v2
{
    public class Game
    {
        private readonly Player[] _playerList;
        private int tries;

        public Game(Player[] players) //ctor makes a Game thingy and draws cards for players to start with
        {
            _playerList = players;
            Deck = new Deck();
            for (int i = 0; i < 5; i++) //5 cards per Player to start
            {
                for (int j = 0; j < _playerList.Length; j++) //card 1 Player 1 2 3 card 2 Player 1 2 3...
                {
                    _playerList[j].Hand.Add(Deck.DrawCard()); //draw a new random card from deck      
                    _playerList[j].Hand.Sort();
                }
            }
            CurrentPlayer = players[0]; //start with Player 1

            PlayArea = new PlayArea(this);
            //be sure the PlayArea can reference the Game variables, pass it along
            PlayArea.Show();
            PlayArea.RoundUpdate(CurrentPlayer);
            //Game has started, thanks ctor
        }

        public Player CurrentPlayer { get; private set; }
        public Deck Deck { get; private set; }
        public PlayArea PlayArea { get; private set; }

        public bool CheckForWin()
            //OMG DID SOMEONE WIN YET? hey know what i found out, this gets really slow after a while... :(
        {
            if (PlayArea.CheckifWinner(CurrentPlayer))
            {
                MessageBox.Show("HOLY CRAP, " + CurrentPlayer.Name + " HAS WON!",
                    "WE GOT A WINNER",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return true;
            }
            return false;
        }

        public void PrepareNextPlayer()
            //Player progression; i'm bad with scoping so here's a hack for some reason i can't remember, probably becakse it's part of the "Game"
        {
            try
            {
                CurrentPlayer = _playerList[CurrentPlayer.Id];//haha, players[0] is Id==1, [1]==2, [2]==3 so catch
            }
            catch
            {
                tries++;
                CurrentPlayer = _playerList[0];
            }
            if (tries > 4000)
            {
                MessageBox.Show(
                    "well, none of you have cards left.  none of you.\r\nall of you lose.  i am ashamed for you.\r\nthe Game will now close because everyone lost.",
                    "unbelievable");
                Environment.Exit(-1);
            }
        }
    }
}
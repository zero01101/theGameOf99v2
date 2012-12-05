namespace theGameOf99v2
{
    public class game
    {
        public playarea playArea;
        public cards gameDeck;
        public player[] playerList;
        public player currentPlayer;
        
        int tries = 0;

        public game(player[] players) //ctor makes a game thingy and draws cards for players to start with
        {
            playerList = players;
            gameDeck = new cards();                        
            for (int i = 0; i < 5; i++) //5 cards per player to start
            {
                for (int j = 0; j < playerList.Length; j++) //card 1 player 1 2 3 card 2 player 1 2 3...
                {
                    playerList[j].hand.Add(gameDeck.draw(gameDeck)); //draw a new random card from deck      
                    playerList[j].hand.Sort();
                }
            }
            currentPlayer = players[0]; //start with player 1
            
            playArea = new playarea(this); //be sure the playarea can reference the game variables, pass it along
            playArea.Show();
            playArea.roundUpdate(currentPlayer);
            //game has started, thanks ctor
        }

        public void prepareNextPlayer() //player progression; i'm bad with scoping so here's a hack for some reason i can't remember, probably becakse it's part of the "game"
        {
            try
            {
                currentPlayer = playerList[currentPlayer.id]; //haha, players[0] is id==1, [1]==2, [2]==3 so catch
                
            }
            catch
            {
                tries++;
                currentPlayer = playerList[0];
            }
            if (tries > 4000)
            {
                System.Windows.Forms.MessageBox.Show("well, none of you have cards left.  none of you.\r\nall of you lose.  i am ashamed for you.\r\nthe game will now close because everyone lost.", "unbelievable");
                System.Environment.Exit(-1);        
            }
        }
        
        public bool checkForWin() //OMG DID SOMEONE WIN YET? hey know what i found out, this gets really slow after a while... :(
        {
            if (playArea.CheckifWinner(currentPlayer))
            {
                System.Windows.Forms.MessageBox.Show("HOLY CRAP, " + currentPlayer.name + " HAS WON!", "WE GOT A WINNER", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return true;
            }
            return false;
        }
    }
}

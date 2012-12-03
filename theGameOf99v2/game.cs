using System;
using System.Linq;
using System.Data;
using System.Collections;

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
            //so this is ugly
            bool gameOver = false;
            if (currentPlayer.ownedSquares.Count > 4)
            {
                ArrayList ownedSquares = currentPlayer.ownedSquares;
                foreach (int squareID in ownedSquares)
                {
                    boardsquare square = (playArea.Controls["bsq" + squareID] as boardsquare);

                    int[] target = { square.row, square.column };
                    int[][] rows = playArea.rows;

                    int vertNegScore = 0;
                    int horzNegScore = 0;
                    int diagForeNegScore = 0;
                    int diagBackNegScore = 0;
                    int vertPosScore = 0;
                    int horzPosScore = 0;
                    int diagForePosScore = 0;
                    int diagBackPosScore = 0; // |up,down --up,down \up,down /up,down scores

                    boardsquare bsqVertPos = null;
                    boardsquare bsqVertNeg = null;
                    boardsquare bsqHorzPos = null;
                    boardsquare bsqHorzNeg = null;
                    boardsquare bsqDiagBackPos = null;
                    boardsquare bsqDiagBackNeg = null;
                    boardsquare bsqDiagForePos = null;
                    boardsquare bsqDiagForeNeg = null; // |up,down --up,down \up,down /up,down square objects

                    for (int i = 0; i < 5; i++) //5 total vert, horiz, and diag back/fore... check both ++ and -- in loop 
                    {
                        if (gameOver == false)
                        {
                            try
                            {
                                int vertneg = rows[square.row + i][square.column];
                                bsqVertNeg = (playArea.Controls["bsq" + vertneg] as boardsquare);
                                vertNegScore = score(bsqVertNeg, vertNegScore);
                            }
                            catch { }

                            try
                            {
                                int vertpos = rows[square.row - i][square.column];
                                bsqVertPos = (playArea.Controls["bsq" + vertpos] as boardsquare);
                                vertPosScore = score(bsqVertPos, vertPosScore);
                            }
                            catch { }

                            try
                            {
                                int horzneg = rows[square.row][square.column + i];
                                bsqHorzNeg = (playArea.Controls["bsq" + horzneg] as boardsquare);
                                horzNegScore = score(bsqHorzNeg, horzNegScore);
                            }
                            catch { }

                            try
                            {
                                int horzpos = rows[square.row][square.column - i];
                                bsqHorzPos = (playArea.Controls["bsq" + horzpos] as boardsquare);
                                horzPosScore = score(bsqHorzPos, horzPosScore);
                            }
                            catch { }

                            try
                            {
                                int diagforeneg = rows[square.row + i][square.column + i];
                                bsqDiagForeNeg = (playArea.Controls["bsq" + diagforeneg] as boardsquare);
                                diagForeNegScore = score(bsqDiagForeNeg, diagForeNegScore);
                            }
                            catch { }

                            try
                            {
                                int diagforepos = rows[square.row - i][square.column - i];
                                bsqDiagForePos = (playArea.Controls["bsq" + diagforepos] as boardsquare);
                                diagForePosScore = score(bsqDiagForePos, diagForePosScore);
                            }
                            catch { }

                            try
                            {
                                int diagbackneg = rows[square.row + i][square.column - i];
                                bsqDiagBackNeg = (playArea.Controls["bsq" + diagbackneg] as boardsquare);
                                diagBackNegScore = score(bsqDiagBackNeg, diagBackNegScore);
                            }
                            catch { }

                            try
                            {
                                int diagbackpos = rows[square.row - i][square.column + i];
                                bsqDiagBackPos = (playArea.Controls["bsq" + diagbackpos] as boardsquare);
                                diagBackPosScore = score(bsqDiagBackPos, diagBackPosScore);
                            }
                            catch { }
                            int[] scores = { vertNegScore, vertPosScore, horzNegScore, horzPosScore, diagForeNegScore, diagForePosScore, diagBackNegScore, diagBackPosScore };
                            foreach (int score in scores)
                            {
                                if (score > 4)
                                {
                                    System.Windows.Forms.MessageBox.Show("HOLY CRAP, " + currentPlayer.name + " HAS WON!", "WE GOT A WINNER", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                                    gameOver = true;
                                    break;
                                }
                            }
                        }                        
                    }
                }
            }
            return gameOver;
        }

        int score(boardsquare squareToCheck, int scoreToAdjust) //score is a verb
        {
            if (squareToCheck.occupant == currentPlayer)
            {
                scoreToAdjust++;
            }
            else
            {
                scoreToAdjust = 0; //have to kill it; if it breaks the chain it's useless to increment at all
            }
            return scoreToAdjust;
        }
    }

}

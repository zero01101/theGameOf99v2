using System;
using System.Collections;

namespace theGameOf99v2
{
    public class cards
    {
        public ArrayList deck = new ArrayList(); //something to contain cards
        Random r = new Random(); //SOOOO RANDOM ^holds up spork^ 

        public cards() //ctor duh
        {
            generate();
        }

        void generate() //make a new deck of cards, size 99, range 1-99
        {
            ArrayList tempDeck = new ArrayList();
            for (int i = 1; i < 100; i++) { tempDeck.Add(i); } //i got 99 cards but 100 aint one           
            Random doubleR = new Random(); //lets shuffle it first
            for (int i = 0; i < 99; i++)
            {
                int rand = r.Next(0, tempDeck.Count);
                deck.Add(tempDeck[rand]);
                tempDeck.Remove(tempDeck[rand]);
            }
        }

        public int draw(cards deck) //this draws the card
        {
            if (deck.deck.Count == 0) //THIS KILLS THE DECK
            {
                System.Windows.Forms.MessageBox.Show("THE DECK IS EMPTY.  REPEAT.  THE DECK HAS RUN DRY.  PANIC MODE COMMENCE.  wait, never mind, new deck generating.","HALT",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);                    
                generate();
                System.Windows.Forms.MessageBox.Show("new deck generated, game on.  note that this will result in, quite literally, doubles of every card.", "carry on", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            int drawncard, cardordinal;
            cardordinal = r.Next(0, deck.deck.Count); //random int between 0 and deck size
            drawncard = (int)deck.deck[cardordinal]; //choose this card by the deck indexer
            deck.deck.Remove(deck.deck[cardordinal]); //this card is no longer usable by anyone else and should now belong to a player's hand                            
            return drawncard; //let caller know what card you just got

        }        
    }
}

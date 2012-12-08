using System;
using System.Collections;
using System.Collections.Generic;

namespace theGameOf99v2
{
    public class Deck
    {
        private readonly List<int> _cards = new List<int>(); //something to contain _cards
        private readonly Random r = new Random(); //SOOOO RANDOM ^holds up spork^ 

        public Deck() //ctor duh
        {
            Generate();
        }

        private void Generate() //make a new _cards of _cards, size 99, range 1-99
        {
            var tempDeck = new List<int>();
            for (int i = 1; i < 100; i++) { tempDeck.Add(i); } //i got 99 _cards but 100 aint one  
         
            for (int i = 0; i < 99; i++)//lets shuffle it first
            {
                int rand = r.Next(0, tempDeck.Count);
                _cards.Add(tempDeck[rand]);
                tempDeck.Remove(tempDeck[rand]);
            }
        }

        public int DrawCard() //this draws the card
        {
            if (_cards.Count == 0) //THIS KILLS THE DECK
            {
                System.Windows.Forms.MessageBox.Show("THE DECK IS EMPTY.  REPEAT.  THE DECK HAS RUN DRY.  PANIC MODE COMMENCE.  wait, never mind, new _cards generating.","HALT",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);                    
                Generate();
                System.Windows.Forms.MessageBox.Show("new _cards generated, Game on.  note that this will result in, quite literally, doubles of every card.", "carry on", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            int cardordinal = r.Next(0, _cards.Count);
            int drawncard = _cards[cardordinal];
            _cards.Remove(_cards[cardordinal]); //this card is no longer usable by anyone else and should now belong to a Player's Hand                            
            return drawncard; //let caller know what card you just got
        }        
    }
}

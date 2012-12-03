using System;
using System.Collections;

namespace theGameOf99v2
{
    public class player
    {
        public string name;
        public int id;
        public ArrayList hand;
        public ArrayList ownedSquares;
        public int selectedCard;

        public player(string Name, int ID) //NEW PLAYAR PLS
        {
            name = Name;
            id = ID;
            hand = new ArrayList();
            ownedSquares = new ArrayList();
        }

        public void removeCard(player player, int cardValueToRemove) //TAKE CARD AWAY PLS
        {
            int remember = -1;
            for (int i = 0; i < player.hand.Count; i++)
            {
                if ((int)player.hand[i] == cardValueToRemove)
                {
                    remember = i;
                }
            }
            player.hand.Remove(player.hand[remember]);
        }
    }
}

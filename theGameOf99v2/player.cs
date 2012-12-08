using System.Collections.Generic;

namespace theGameOf99v2
{
    public class Player
    {
        public Player(string name, int id) //NEW PLAYAR PLS
        {
            Name = name;
            Id = id;
            Hand = new List<int>();
            OwnedSquares = new List<int>();
        }

        public List<int> Hand { get; private set; }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<int> OwnedSquares { get; private set; }
        public int SelectedCard { get; set; }

        public void RemoveCardSelectedCard() //TAKE CARD AWAY PLS
        {
            int remember = -1;
            for (int i = 0; i < Hand.Count; i++)
            {
                if (Hand[i] == SelectedCard)
                {
                    remember = i;
                }
            }
            Hand.Remove(Hand[remember]);
        }
    }
}
using System.Drawing;
using System.Windows.Forms;
using theGameOf99v2.Interfaces;

namespace theGameOf99v2
{
    public class BoardSquare : Button, ISelectable
    {
        private readonly int _squareId;
        private Player _occupant;

        public BoardSquare(int squareId)
        {
            _squareId = squareId;
        }

        public int Id { get { return _squareId; } }
        public Player Occupant { get { return _occupant; } }
        public bool Occupied { get { return _occupant != null; } }

        public bool Occupy(Player player) //the only thing you can really do to a BoardSquare object
        {
            if (DialogResult.Yes == MessageBox.Show("are you sure you wish to Occupy square " + Id + "?", "HALT", MessageBoxButtons.YesNo))
            {
                switch (player.Id)
                {
                    case 1:
                        BackColor = Color.Red;
                        break;
                    case 2:
                        BackColor = Color.Blue;
                        break;
                    case 3:
                        BackColor = Color.Green;
                        break;
                }
                Enabled = false;
                _occupant = player;
                player.OwnedSquares.Add(Id);
                player.RemoveCardSelectedCard();
            }
            return Occupied;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace theGameOf99v2
{
    public class boardsquare : Button
    {
        public int squareID;
        public int row;
        public int column;
        public player occupant;
        public bool occupied = false;

        public bool occupy(object sender, player player, int selectedCard) //the only thing you can really do to a boardsquare object
        {
            boardsquare square = (sender as boardsquare);
            DialogResult result = MessageBox.Show("are you sure you wish to occupy square " + square.squareID.ToString() + "?", "HALT", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                switch (player.id)
                {
                    case 1:
                        square.BackColor = System.Drawing.Color.Red;
                        break;
                    case 2:
                        square.BackColor = System.Drawing.Color.Blue;
                        break;
                    case 3:
                        square.BackColor = System.Drawing.Color.Green;
                        break;
                }
                square.Enabled = false;
                square.occupied = true;
                square.occupant = player;
                player.ownedSquares.Add(square.squareID);
                player.removeCard(player, selectedCard);
            }
            return square.occupied;
        }
    }
}

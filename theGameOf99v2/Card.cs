using System.Windows.Forms;
using theGameOf99v2.Interfaces;

namespace theGameOf99v2
{
    public class Card : RadioButton, ISelectable
    {
        public int Id { get; set; }
        public Card()
        {

        }
    }
}
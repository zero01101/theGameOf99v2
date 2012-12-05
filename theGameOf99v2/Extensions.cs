using System.Windows.Forms;
using System.Linq;

namespace theGameOf99v2
{
    public static class Extensions
    {
        public static boardsquare GetSquare(this Control.ControlCollection collection,  int squareId)
        {
            return collection.OfType<boardsquare>().First(x => x.squareID == squareId);
        }
    }
}
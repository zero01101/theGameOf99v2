namespace theGameOf99v2.Interfaces
{
    public interface ISquareCollection
    {
        bool CheckIfWinner(Player player);
        BoardSquare this[int index] { get; }
    }
}
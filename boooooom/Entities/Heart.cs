namespace boooooom.Entities;


public class Heart : Prize
{
    public Heart() : base(1) { }

    public override string GetDrawSymbol()
    {
        return "❤️";
    }

    public override string GetDrawImage()
    {
        return "Heart"; // Replace with actual image path or identifier
    }
}
public class Prize
{
    public int Score { get; private set; }
    public bool IsCollected { get; private set; }

    public Prize(int score)
    {
        Score = score;
        IsCollected = false;
    }

    public virtual string GetDrawSymbol()
    {
        if (IsCollected)
        {
            return " ";
        }

        return "🐟";
    }

    public virtual void Collect()
    {
        IsCollected = true;
    }

    public virtual string GetDrawImage()
    {
        return "Prize"; // Replace with actual image path or identifier
    }
}

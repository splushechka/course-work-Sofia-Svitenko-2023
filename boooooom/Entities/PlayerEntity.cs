using boooooom.Entities;
using boooooom.Enums;

public class PlayerEntity : ActiveEntity
{
    public PlayerEntity()
    {
        Lives = 3;
    }

    public override ActiveEntityType GetEntityType()
    {
        return ActiveEntityType.Player;
    }

    public override ActiveEntity Clone()
    {
        return new PlayerEntity() { Lives = Lives };
    }

    public string GetDrawSymbol(bool hasBomb, bool hasPrize, bool isAffectedByExplosion)
    {
        if (hasBomb)
        {
            return "🙀"; // Scared cat symbol when on the same cell with a bomb
        }
        else if (isAffectedByExplosion)
        {
            return "😿"; // Sad emoji when on the same cell with an enemy
        }
        else if (hasPrize)
        {
            return "😻"; // Loving cat when player and prize are on the same cell
        }
        return "😸"; // Cat figure when player is on the cell
    }

    public override string GetDrawImage()
    {
        return "Player"; // Replace with actual image path or identifier
    }
    public string GetDrawImage(bool hasBomb, bool hasPrize, bool isAffectedByExplosion)
    {
        if (hasBomb)
        {
            return "ScaredPlayer"; // Scared cat symbol when on the same cell with a bomb
        }
        else if (isAffectedByExplosion)
        {
            return "SadPlayer"; // Sad emoji when on the same cell with an enemy
        }
        else if (hasPrize)
        {
            return "HappyPlayer"; // Loving cat when player and prize are on the same cell
        }

        return "Player"; // Cat figure when player is on the cell
    }
}
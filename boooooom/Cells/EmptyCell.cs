using boooooom.Entities;
using boooooom.Entities.Enemies;
using boooooom.Enums;
using System.Collections.Generic;
using System.Linq;

namespace boooooom.Cells
{
    public class EmptyCell : Cell
    {
        public bool Exploded { get; private set; }

        public EmptyCell(bool isPlayerOnCell = false) : base(CellType.Empty)
        {
            if (isPlayerOnCell)
            {
                EntitiesOnCell.Add(new PlayerEntity());
            }

            Exploded = false;
            BombOnCell = null;
        }

        public override bool CanActiveEntityStep()
        {
            return true;
        }

        public override bool HasPrize(out int prizeValue)
        {
            if (PrizeOnCell != null)
            {
                prizeValue = PrizeOnCell.Score;
                return true;
            }
            else
            {
                prizeValue = 0;
                return false;
            }
        }

        public override string GetDrawSymbol()
        {
            bool hasPlayer = EntitiesOnCell.Any(e => e is PlayerEntity);
            bool hasBomb = BombOnCell != null;
            bool hasPrize = PrizeOnCell != null;

            foreach (var entity in EntitiesOnCell)
            {
                if (entity is Enemy enemy)
                {
                    return enemy.GetDrawSymbol(); // Call GetDrawSymbol method for enemy
                }
                else if (entity is PlayerEntity playerEntity)
                {
                    return playerEntity.GetDrawSymbol(hasBomb, hasPrize, IsAffectedByExplosion); // Call GetDrawSymbol method for player
                }
            }

            if (BombOnCell != null)
            {
                return BombOnCell.GetDrawSymbol();
            }
            else if (IsAffectedByExplosion)
            {
                return "💥";
            }
            else if (PrizeOnCell != null)
            {
                return PrizeOnCell.GetDrawSymbol(); // Display prize symbol
            }

            return "  ";
        }

        public override bool CanCellExplode()
        {
            return true;
        }

        public void PlaceBomb()
        {
            if (BombOnCell == null)
            {
                BombOnCell = new Bomb();
            }
        }

        public override string GetDrawImage()
        {
            bool hasPlayer = EntitiesOnCell.Any(e => e is PlayerEntity);
            bool hasBomb = BombOnCell != null;
            bool hasPrize = PrizeOnCell != null;

            foreach (var entity in EntitiesOnCell)
            {
                if (entity is Enemy enemy)
                {
                    return enemy.GetDrawImage(); // Call GetDrawImage method for enemy
                }
                else if (entity is PlayerEntity playerEntity)
                {
                    return playerEntity.GetDrawImage(hasBomb, hasPrize, IsAffectedByExplosion); // Call GetDrawImage method for player
                }
            }

            if (BombOnCell != null)
            {
                return BombOnCell.GetDrawImage();
            }
            else if (IsAffectedByExplosion)
            {
                return "Explosion";
            }
            else if (PrizeOnCell != null)
            {
                return PrizeOnCell.GetDrawImage(); // Display prize image
            }

            return "EmptyCell";
        }
    }
}

using System.Linq;
using Zenject;

public class MatchTransformator : IMatchTransformator
{
    [Inject] private IPieceInstantiator pieceInstantiator;

    public IPiece Transform(IMatchInfo matchInfo)
    {
        if (matchInfo.MovedContainer.Piece == null || matchInfo.MatchedContainers.Any(x => x.Piece.Type != PieceType.Common)) return null;

        IPiece piece = null;
        PieceColor pieceColor = matchInfo.MovedContainer.Piece.Color;
        PieceType pieceType = GetPieceType(matchInfo);

        if(pieceType != PieceType.Common)
        {
            piece = pieceInstantiator.Instantiate(pieceColor, pieceType);
            piece.SetPosition(matchInfo.MovedContainer.Position);
        }

        return piece;
    }

    public PieceType GetPieceType(IMatchInfo matchInfo)
    {
        if (matchInfo.VerticalContainers.Count() > 2 && matchInfo.HorizontalContainers.Count() > 2)
        {
            return PieceType.Bomb;
        }

        if (matchInfo.VerticalContainers.Count() > 3)
        {
            return PieceType.Vertical;
        }

        if (matchInfo.HorizontalContainers.Count() > 3)
        {
            return PieceType.Horizontal;
        }


        return PieceType.Common;
    }
}
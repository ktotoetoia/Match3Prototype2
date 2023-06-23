using System.Linq;
using Zenject;

public class MatchTransformator : IMatchTransformator
{
    [Inject] private IPieceInstantiator pieceInstantiator;

    public IPiece Transform(IMatchInfo matchInfo)
    {
        IPiece movedPiece = matchInfo.MovedContainer.Piece;

        if (movedPiece == null || 
            matchInfo.MatchedContainers
            .Where(x=> x!= null)
            .Any(x => x.Piece?.Type != PieceType.Common))
        {
            return null;
        }

        PieceType pieceType = GetPieceType(matchInfo);

        if (pieceType == PieceType.Common)
        {
            return null;
        }

        PieceColor pieceColor = movedPiece.Color;
        IPiece piece = pieceInstantiator.Instantiate(pieceColor, pieceType);
        piece.SetPosition(matchInfo.MovedContainer.Position);

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
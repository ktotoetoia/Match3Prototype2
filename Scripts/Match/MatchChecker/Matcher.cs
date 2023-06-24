using Zenject;
using System.Linq;

public class Matcher : IMatcher
{
    private IMatchTransformator matchTransformator;

    public Matcher(IMatchTransformator matchTransformator)
    {
        this.matchTransformator = matchTransformator;
    }

    public void MatchContainers(IMatchInfo matchInfo)
    {
        IPiece piece = matchTransformator.Transform(matchInfo);

        if (piece != null && !matchInfo.MatchedContainers.Any(x => x.Piece.Type != PieceType.Common))
        {
            matchInfo.MovedContainer.TransformToPiece(matchInfo, piece);
            matchInfo.RemoveContainer(matchInfo.MovedContainer);
        }

        foreach (IPieceContainer container in matchInfo.MatchedContainers)
        {
            container.OnMatch(matchInfo);
        }
    }
}
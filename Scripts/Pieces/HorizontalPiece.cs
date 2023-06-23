using System.Collections.Generic;

public class HorizontalPiece : Piece
{
    private bool isMatched = false;

    public override void OnMatch(IMatchInfo matchInfo)
    {
        if (isMatched) return;
        isMatched = true;

        IPieceContainer container = matchInfo.GetContainer(this);
        matchInfo.MatchChecker.CheckHorizontalMatch(container);

        base.OnMatch(matchInfo);
    }
}
using System.Collections.Generic;

public class BombPiece : Piece
{
    private bool isMatched = false;

    public override void OnMatch(IMatchInfo matchInfo)
    {
        if(isMatched) return;

        isMatched = true;

        IPieceContainer container = matchInfo.GetContainer(this);
        matchInfo.MatchChecker.CheckSquareMatch(container);
        
        base.OnMatch(matchInfo);
    }
}
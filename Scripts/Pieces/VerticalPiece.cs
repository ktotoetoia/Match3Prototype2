public class VerticalPiece : Piece
{
    private bool isMatched = false;

    public override void OnMatch(IMatchInfo matchInfo)
    {
        if (isMatched) return;
        isMatched = true;

        IPieceContainer container = matchInfo.GetContainer(this);
        matchInfo.MatchChecker.CheckVerticalMatch(container);
        
        base.OnMatch(matchInfo);
    }
}
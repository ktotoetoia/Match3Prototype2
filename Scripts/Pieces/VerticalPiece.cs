public class VerticalPiece : Piece
{
    public override PieceType Type => PieceType.Vertical;

    public override void OnMatch(IMatchInfo matchInfo,IPieceContainer container)
    {
        matchInfo.MatchChecker.CheckVerticalMatch(container);
        
        base.OnMatch(matchInfo, container);
    }
}
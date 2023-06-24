public class HorizontalPiece : Piece
{
    public override PieceType Type => PieceType.Horizontal;

    public override void OnMatch(IMatchInfo matchInfo,IPieceContainer container)
    {
        matchInfo.MatchChecker.CheckHorizontalMatch(container);

        base.OnMatch(matchInfo, container);
    }
}
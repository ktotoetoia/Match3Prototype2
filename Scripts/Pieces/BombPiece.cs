public class BombPiece : Piece
{
    public override PieceType Type => PieceType.Bomb;

    public override void OnMatch(IMatchInfo matchInfo,IPieceContainer container)
    {
        matchInfo.MatchChecker.CheckSquareMatch(container);
        
        base.OnMatch(matchInfo,container);
    }
}
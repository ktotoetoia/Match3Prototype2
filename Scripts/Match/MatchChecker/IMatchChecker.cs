public interface IMatchChecker
{
    public IMatchInfoFactory MatchInfoFactory { get; }

    public void CheckMatch(IPieceContainer from, IPieceContainer to);
    void CheckHorizontalMatch(IPieceContainer container);
    void CheckSquareMatch(IPieceContainer container);
    void CheckVerticalMatch(IPieceContainer container);
}
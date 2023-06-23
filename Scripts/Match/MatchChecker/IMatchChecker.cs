public interface IMatchChecker
{
    public IMatchInfoFactory MatchInfoFactory { get; }
    IMatcher Matcher { get; }
    IMatchGetter MatchGetter { get; }

    public void CheckMatch(IPieceContainer from, IPieceContainer to);
    void CheckMatches(IMatchInfo matchInfo);
    void CheckHorizontalMatch(IPieceContainer container);
    void CheckSquareMatch(IPieceContainer container);
    void CheckVerticalMatch(IPieceContainer container);
}
using System.Collections.Generic;

public interface IMatchInfo
{
    public IPieceContainer MovedContainer { get; }
    public IMatchChecker MatchChecker { get; }

    public IEnumerable<IPieceContainer> VerticalContainers { get; }
    public IEnumerable<IPieceContainer> HorizontalContainers { get; } 
    public IEnumerable<IPieceContainer> MatchedContainers { get; }

    public void RemoveContainer(IPieceContainer container);
    public IPieceContainer GetContainer(IPiece piece);
}
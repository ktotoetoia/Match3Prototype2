using System.Collections.Generic;

public interface IMatchInfo
{
    public IPieceContainer MovedContainer { get; }
    public List<IPieceContainer> GetContainersToMatch();

    public void RemoveContainer(IPieceContainer container);
}
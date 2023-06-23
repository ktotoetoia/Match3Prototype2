using System.Collections.Generic;

public interface IMatchInfoFactory
{
    public IMatchInfo Create(IPieceContainer container, List<IPieceContainer> verticalContainers, List<IPieceContainer> horizontalContainers);
    public IMatchInfo Create(IPieceContainer container, List<IPieceContainer> matchedContainers);
}
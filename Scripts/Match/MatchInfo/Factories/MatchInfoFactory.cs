using System.Collections.Generic;

public class MatchInfoFactory : IMatchInfoFactory
{
    private IMatchChecker matchChecker;
    
    public MatchInfoFactory(IMatchChecker matchChecker)
    {
        this.matchChecker = matchChecker;
    }

    public IMatchInfo Create(IPieceContainer container, List<IPieceContainer> vertical, List<IPieceContainer> horizontal)
    {
        MatchInfo matchInfo = new MatchInfo(container,vertical,horizontal);

        InstantiateProperties(matchInfo);
        
        return matchInfo;
    }

    public IMatchInfo Create(IPieceContainer container,List<IPieceContainer> matchedContainers)
    {
        MatchInfo matchInfo = new MatchInfo(container, matchedContainers);

        InstantiateProperties(matchInfo);

        return matchInfo;
    }

    private void InstantiateProperties(MatchInfo matchInfo)
    {
        matchInfo.MatchChecker = matchChecker;
    }
}
using System.Collections.Generic;

public class MatchInfo : IMatchInfo
{
    private List<IPieceContainer> vertical;
    private List<IPieceContainer> horizontal;

    private List<IPieceContainer> matchContainers = new List<IPieceContainer>();

    public IPieceContainer MovedContainer { get; private set; }

    public MatchInfo(IPieceContainer container, List<IPieceContainer> vertical, List<IPieceContainer> horizontal)
    {
        MovedContainer = container;

        this.vertical = vertical;
        this.horizontal = horizontal;

        SetContainersToMatch();
    }

    private void SetContainersToMatch()
    {
        AddToResultIfMatch(horizontal);
        AddToResultIfMatch(vertical);
    }

    private void AddToResultIfMatch(List<IPieceContainer> containers)
    {
        if (containers.Count > 2)
        {
            matchContainers.AddRange(containers);
        }
    }

    public List<IPieceContainer> GetContainersToMatch()
    {
        return matchContainers;
    }

    public void RemoveContainer(IPieceContainer container)
    {
        matchContainers.Remove(container);
    }
}
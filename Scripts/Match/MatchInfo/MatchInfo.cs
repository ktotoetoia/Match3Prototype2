using System.Collections.Generic;
using System.Linq;

public class MatchInfo : IMatchInfo
{
    private List<IPieceContainer> verticalContainers = new List<IPieceContainer>();
    private List<IPieceContainer> horizontalContainers = new List<IPieceContainer>();
    private List<IPieceContainer> matchedContainers = new List<IPieceContainer>();

    public IPieceContainer MovedContainer { get; private set; }
    public IMatchChecker MatchChecker { get; set; }

    public IEnumerable<IPieceContainer> VerticalContainers
    {
        get
        {
            foreach (IPieceContainer container in verticalContainers) yield return container;
        }
    }

    public IEnumerable<IPieceContainer> HorizontalContainers
    {
        get
        {
            foreach (IPieceContainer container in horizontalContainers) yield return container;
        }
    }

    public IEnumerable<IPieceContainer> MatchedContainers
    {
        get
        {
            foreach (IPieceContainer container in matchedContainers) yield return container;
        }
    }

    public MatchInfo(IPieceContainer container, List<IPieceContainer> matchedContainers)
    {
        MovedContainer = container;
        this.matchedContainers = matchedContainers;
    }

    public MatchInfo(IPieceContainer container, List<IPieceContainer> vertical, List<IPieceContainer> horizontal)
    {
        MovedContainer = container;

        verticalContainers.AddRange(vertical);
        horizontalContainers.AddRange(horizontal);

        SetContainersToMatch();
    }

    private void SetContainersToMatch()
    {
        AddToResultIfMatch(horizontalContainers);
        AddToResultIfMatch(verticalContainers);

        matchedContainers = matchedContainers.Distinct().ToList();
    }

    private void AddToResultIfMatch(List<IPieceContainer> containers)
    {
        if (containers.Count > 2)
        {
            matchedContainers.AddRange(containers);
        }
    }

    public void RemoveContainer(IPieceContainer container)
    {
        matchedContainers.Remove(container);
    }

    public IPieceContainer GetContainer(IPiece piece)
    {
        return matchedContainers.Find(x => x.Piece == piece);
    }
}
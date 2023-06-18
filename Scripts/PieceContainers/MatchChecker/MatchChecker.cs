using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class MatchChecker : MonoBehaviour, IMatchChecker
{
    private delegate IPieceContainer GetNextContainer(IPieceContainer container);

    public void CheckMatch(IPieceContainer from, IPieceContainer to)
    {
        List<IPieceContainer> fromContainers = GetMatchedContainers(from);
        List<IPieceContainer> toContainers = GetMatchedContainers(to);

        fromContainers.Remove(to);
        toContainers.Remove(from);

        List<IEnumerator> coroutines = new List<IEnumerator>
        { 
            CheckMatchesWhenPieceArrives(from,fromContainers),
            CheckMatchesWhenPieceArrives(to, toContainers),
            UpdateAfterMatch(fromContainers.Concat(toContainers)),
        };

        StartCoroutinesInOrder(coroutines);
    }

    public void StartCoroutinesInOrder(IEnumerable<IEnumerator> coroutines)
    {
        StartCoroutine(RunCoroutinesInOrder(coroutines));
    }

    private IEnumerator RunCoroutinesInOrder(IEnumerable<IEnumerator> coroutines)
    {
        foreach (IEnumerator coroutine in coroutines)
        {
            yield return StartCoroutine(coroutine);
        }
    }

    private IEnumerator CheckMatchesWhenPieceArrives(IPieceContainer container, List<IPieceContainer> containers)
    {
        IPiece piece = container.Piece;

        yield return new WaitUntil(() => piece.Arrived);
        
        MatchPieces(containers);
    }

    private IEnumerator UpdateAfterMatch(IEnumerable<IPieceContainer> containers)
    {
        UpdateContainers(containers);
        yield return null;
    }

    private List<IPieceContainer> GetMatchedContainers(IPieceContainer container)
    {
        List<IPieceContainer> verticalContainers = VerticalMatchs(container);
        List<IPieceContainer> horizontalContainers = HorizontalMatchs(container);

        List<IPieceContainer> result = new List<IPieceContainer>();

        if(horizontalContainers.Count > 2)
        {
            result.AddRange(horizontalContainers);
        }

        if (verticalContainers.Count > 2)
        {
            result.AddRange(verticalContainers);
        }

        return result;
    }

    private void MatchPieces(IEnumerable<IPieceContainer> containers)
    {
        foreach(IPieceContainer container in containers)
        {
            container?.OnMatch();
        }
    }

    private void UpdateContainers(IEnumerable<IPieceContainer> containers)
    {
        foreach (IPieceContainer container in containers)
        {
            container?.Update();
        }
    }

    private List<IPieceContainer> VerticalMatchs(IPieceContainer container)
    {
        List<IPieceContainer> containers = new List<IPieceContainer>
        {
            container
        };
        
        UpMatchs(container, containers);
        DownMatchs(container, containers);

        return containers;
    }

    private List<IPieceContainer> HorizontalMatchs(IPieceContainer container)
    {
        List<IPieceContainer> containers = new List<IPieceContainer>
        {
            container
        };

        LeftMatchs(container, containers);
        RightMatchs(container, containers);

        return containers;
    }

    private void UpMatchs(IPieceContainer container, List<IPieceContainer> containers)
    {
        MatchContainers(container, containers, c => c?.IncidentContainerInfo?.Up);
    }

    private void DownMatchs(IPieceContainer container, List<IPieceContainer> containers)
    {
        MatchContainers(container, containers, c => c?.IncidentContainerInfo?.Down);
    }

    private void LeftMatchs(IPieceContainer container, List<IPieceContainer> containers)
    {
        MatchContainers(container, containers, c => c?.IncidentContainerInfo?.Left);
    }

    private void RightMatchs(IPieceContainer container, List<IPieceContainer> containers)
    {
        MatchContainers(container, containers, c => c?.IncidentContainerInfo?.Right);
    }

    private void MatchContainers(IPieceContainer container, List<IPieceContainer> containers, GetNextContainer getNextContainer)
    {
        IPieceContainer nextContainer = getNextContainer(container);
        
        if (container != null && nextContainer?.Piece?.Color == container?.Piece?.Color)
        {
            containers.Add(nextContainer);
            MatchContainers(nextContainer, containers, getNextContainer);
        }
    }
}
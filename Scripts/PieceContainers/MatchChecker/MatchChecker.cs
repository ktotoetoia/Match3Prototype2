using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class MatchChecker : MonoBehaviour, IMatchChecker
{
    private delegate IPieceContainer GetNextContainer(IPieceContainer container);

    public void CheckMatch(IPieceContainer from, IPieceContainer to)
    {
        IMatchInfo fromInfo = GetMatchInfo(from);
        IMatchInfo toInfo = GetMatchInfo(to);

        fromInfo.RemoveContainer(to);
        toInfo.RemoveContainer(from);

        StartCoroutines(fromInfo, toInfo);
    }

    private void StartCoroutines(IMatchInfo fromInfo, IMatchInfo toInfo)
    {
        List<IPieceContainer> fromContainers = fromInfo.GetContainersToMatch();
        List<IPieceContainer> toContainers = toInfo.GetContainersToMatch();

        List<IEnumerator> coroutines = new List<IEnumerator>
        {
            CheckMatchesWhenPieceArrives(fromInfo),
            CheckMatchesWhenPieceArrives(toInfo),
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

    private IEnumerator CheckMatchesWhenPieceArrives(IMatchInfo matchInfo)
    {
        IPiece piece = matchInfo.MovedContainer.Piece;

        yield return new WaitUntil(() => piece.Arrived);
        
        MatchPieces(matchInfo.GetContainersToMatch());
    }

    private IEnumerator UpdateAfterMatch(IEnumerable<IPieceContainer> containers)
    {
        UpdateContainers(containers);
        yield return null;
    }

    private IMatchInfo GetMatchInfo(IPieceContainer container)
    {
        List<IPieceContainer> verticalContainers = new List<IPieceContainer>();
        List<IPieceContainer> horizontalContainers = new List<IPieceContainer>();

        VerticalMatchs(container,verticalContainers);
        HorizontalMatchs(container, horizontalContainers);

        return new MatchInfo(container, verticalContainers, horizontalContainers);
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

    private void VerticalMatchs(IPieceContainer container, List<IPieceContainer> containers)
    {
        containers.Add(container);
        UpMatchs(container, containers);
        DownMatchs(container, containers);
    }

    private void HorizontalMatchs(IPieceContainer container,List<IPieceContainer> containers)
    {
        containers.Add(container);
        LeftMatchs(container, containers);
        RightMatchs(container, containers);
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
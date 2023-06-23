using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Zenject;

public class MatchChecker : MonoBehaviour, IMatchChecker
{
    public IMatchInfoFactory MatchInfoFactory { get; private set; }

    [Inject] private IMatchGetter matchGetter;
    [Inject] private IMatcher matcher;

    private void Awake()
    {
        MatchInfoFactory = new MatchInfoFactory(this);
    }

    public void CheckMatch(IPieceContainer from, IPieceContainer to)
    {
        IMatchInfo fromInfo = GetMatchInfo(from);
        IMatchInfo toInfo = GetMatchInfo(to);

        fromInfo.RemoveContainer(to);
        toInfo.RemoveContainer(from);

        StartMatchCoroutines(fromInfo, toInfo);
    }

    private void StartMatchCoroutines(params IMatchInfo[] matchInfos)
    {
        List<IPieceContainer> containersToUpdate = new List<IPieceContainer>();
        List<IEnumerator> coroutines = new List<IEnumerator>();

        for (int i = 0; i < matchInfos.Length; i++)
        {
            coroutines.Add(CheckMatchesWhenPieceArrives(matchInfos[i]));
            containersToUpdate.AddRange(matchInfos[i].MatchedContainers);
        }

        coroutines.Add(UpdateInNextFrame(containersToUpdate));
        
        StartCoroutinesInOrder(coroutines);
    }

    private void StartCoroutinesInOrder(IEnumerable<IEnumerator> coroutines)
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
     
        if (piece!= null)
        {
            yield return new WaitUntil(() => piece.Arrived);

            matcher.MatchContainers(matchInfo);
        }
    }

    private IEnumerator UpdateInNextFrame(IEnumerable<IPieceContainer> containers)
    {
        yield return null;
        UpdateContainers(containers);
    }

    private IMatchInfo GetMatchInfo(IPieceContainer container)
    {
        List<IPieceContainer> verticalContainers = matchGetter.GetVerticalMatchs(container);

        List<IPieceContainer> horizontalContainers = matchGetter.GetHorizontalMatchs(container);

        return MatchInfoFactory.Create(container,verticalContainers,horizontalContainers);
    }

    private void UpdateContainers(IEnumerable<IPieceContainer> containers)
    {
        foreach (IPieceContainer container in containers)
        {
            container?.Update();
        }
    }

    public void CheckSquareMatch(IPieceContainer container)
    {
        List<IPieceContainer> containersToMatch = matchGetter.GetSquareMatch(container, 2);
        CreateMatchInfo(container, containersToMatch);
    }

    public void CheckHorizontalMatch(IPieceContainer container)
    {
        List<IPieceContainer> containersToMatch = matchGetter.GetHorizontalMatchs(container,true);
        CreateMatchInfo(container, containersToMatch);
    }

    public void CheckVerticalMatch(IPieceContainer container)
    {
        List<IPieceContainer> containersToMatch = matchGetter.GetVerticalMatchs(container, true);
        CreateMatchInfo(container, containersToMatch);
    }

    private void CreateMatchInfo(IPieceContainer container, List<IPieceContainer> containersToMatch)
    {
        IMatchInfo matchInfo = MatchInfoFactory.Create(container, containersToMatch);
        matchInfo.RemoveContainer(container);
        StartMatchCoroutines(matchInfo);
    }
}
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;

public class MatchChecker : MonoBehaviour, IMatchChecker
{
    public IMatchInfoFactory MatchInfoFactory { get; private set; }

    [Inject] public IMatchGetter MatchGetter { get; private set; }
    
    [Inject] public IMatcher Matcher { get; private set; }

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

        StartCoroutines(fromInfo, toInfo);
    }

    public void CheckMatches(IMatchInfo matchInfo)
    {
        Matcher.MatchContainers(matchInfo);
        UpdateContainers(matchInfo.MatchedContainers);
    }

    private void StartCoroutines(params IMatchInfo[] matchInfos)
    {
        List<IEnumerator> coroutines = new List<IEnumerator>();
        List<IPieceContainer> containersToUpdate = new List<IPieceContainer>();

        for (int i = 0; i< matchInfos.Length; i++)
        {
            coroutines.Add(CheckMatchesWhenPieceArrives(matchInfos[i]));
            containersToUpdate.AddRange(matchInfos[i].MatchedContainers);
        }

        coroutines.Add(UpdateAfterMatch(containersToUpdate));
        
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
     
        if (piece!= null)
        {
            yield return new WaitUntil(() => piece.Arrived);

            Matcher.MatchContainers(matchInfo);
        }
    }

    private IEnumerator UpdateAfterMatch(IEnumerable<IPieceContainer> containers)
    {
        UpdateContainers(containers);
        yield return null;
    }

    private IMatchInfo GetMatchInfo(IPieceContainer container)
    {
        List<IPieceContainer> verticalContainers = MatchGetter.GetVerticalMatchs(container);

        List<IPieceContainer> horizontalContainers = MatchGetter.GetHorizontalMatchs(container);

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
        List<IPieceContainer> containersToMatch = MatchGetter.GetSquareMatch(container, 2);
        CreateMatchInfo(container, containersToMatch);
    }

    public void CheckHorizontalMatch(IPieceContainer container)
    {
        List<IPieceContainer> containersToMatch = MatchGetter.GetHorizontalMatchs(container,true);
        CreateMatchInfo(container, containersToMatch);
    }

    public void CheckVerticalMatch(IPieceContainer container)
    {
        List<IPieceContainer> containersToMatch = MatchGetter.GetVerticalMatchs(container, true);
        CreateMatchInfo(container, containersToMatch);
    }

    private void CreateMatchInfo(IPieceContainer container, List<IPieceContainer> containersToMatch)
    {
        IMatchInfo matchInfo = MatchInfoFactory.Create(container, containersToMatch);
        
        StartCoroutines(matchInfo);
    }
}
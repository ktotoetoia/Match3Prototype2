using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IncidentContainersInfo : IIncidentContainersInfo
{
    private IPieceContainer container;
    private List<IPieceContainer> incidentContainers = new List<IPieceContainer>();
    
    private List<IPieceAction> pieceActions = new List<IPieceAction>();
    private List<IPieceAction> latePieceActions = new List<IPieceAction>();

    public event IIncidentContainersInfo.IncidentContainerDelegate ContainerActionsUpdated;
    public event IIncidentContainersInfo.IncidentContainerDelegate IncidentContainersUpdated;

    public IPieceContainer Up { get; private set; }
    public IPieceContainer Down { get; private set; }
    public IPieceContainer Left { get; private set; }
    public IPieceContainer Right { get; private set; }

    public IEnumerable<IPieceContainer> IncidentContainers
    {
        get
        {
            foreach(IPieceContainer container in incidentContainers) yield return container;
        }
    }

    public IncidentContainersInfo(IPieceContainer container)
    {
        this.container = container;
    }

    public void AddContainer(IPieceContainer containerToAdd)
    {
        Vector2Int thisContainerPosition = container.ContainerInfo.GraphPosition;
        Vector2Int containerToAddPosition = containerToAdd.ContainerInfo.GraphPosition;

        if (thisContainerPosition.x == containerToAddPosition.x || thisContainerPosition.y == containerToAddPosition.y)
        {
            AddContainerDirection(containerToAdd);

            incidentContainers.Add(containerToAdd);
        }
    }

    private void AddContainerDirection(IPieceContainer containerToAdd)
    {
        Vector2Int thisContainerPosition = container.ContainerInfo.GraphPosition;
        Vector2Int containerToAddPosition = containerToAdd.ContainerInfo.GraphPosition;

        if (thisContainerPosition.y > containerToAddPosition.y) Down = containerToAdd;
        if (thisContainerPosition.y < containerToAddPosition.y) Up = containerToAdd;
        if (thisContainerPosition.x > containerToAddPosition.x) Left = containerToAdd;
        if (thisContainerPosition.x < containerToAddPosition.x) Right = containerToAdd;
    }

    public bool IsIncident(IPieceContainer container)
    {
        return incidentContainers.Contains(container);
    }

    public void Update()
    {   
        foreach (IPieceContainer container in IncidentContainers)
        {
            container.IncidentContainerInfo.UpdateActions();
        }

        IncidentContainersUpdated?.Invoke(this);
    }

    public void UpdateActions()
    {
        foreach (IPieceAction actions in pieceActions)
        {
            actions.Update();
        }

        ContainerActionsUpdated?.Invoke(this);
    }

    public void LateUpdateActions()
    {
        foreach (IPieceAction action in latePieceActions)
        {
            action.Update();
        }
    }

    public void AddIncidentContainer(IPieceContainer container)
    {
        if (container is NullContainer)
        {
            return;
        }

        AddContainerToActions(container);
    }

    private void AddContainerToActions(IPieceContainer container)
    {
        AddContainer(container);

        foreach (IPieceAction action in pieceActions.Concat(latePieceActions))
        {
            action.AddContainer(container);
        }
    }

    public void AddAction(IPieceAction action)
    {
        pieceActions.Add(action);
    }

    public void AddLateAction(IPieceAction lateAction)
    {
        latePieceActions.Add(lateAction);
    }
}
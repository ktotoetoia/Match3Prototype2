using System.Collections.Generic;

public interface IPieceAction : ICanAddContainer
{
    public void Update();
}

public interface IIncidentContainersInfo : ICanAddContainer, IGraphUpdatable
{
    public delegate void IncidentContainerDelegate(IIncidentContainersInfo incidentContainerInfo);

    public event IncidentContainerDelegate ContainerActionsUpdated;
    public event IncidentContainerDelegate IncidentContainersUpdated;
    
    public IPieceContainer Up { get; }
    public IPieceContainer Down { get;}
    public IPieceContainer Left { get;}
    public IPieceContainer Right { get; }

    public IEnumerable<IPieceContainer> IncidentContainers { get; }
    public bool IsIncident(IPieceContainer container);
    public void AddIncidentContainer(IPieceContainer container);
    public void UpdateActions();
    public void LateUpdateActions();
    public void AddAction(IPieceAction action);
    public void AddLateAction(IPieceAction lateAction);
}

public interface ICanAddContainer
{
    public void AddContainer(IPieceContainer container);
}
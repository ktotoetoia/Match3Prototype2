using UnityEngine;
using Zenject;

public class PieceContainerMover : IPieceContainerMover
{
    [Inject] private IPieceContainerHover containerHover;
    [Inject] private IGraphInfo graphInfo;
    [Inject] private IGraph graph;

    public IPieceContainer selectedContainer;

    public void OnSelect()
    {
        IPieceContainer container = containerHover.GetClosestToMouseContainer();

        if (Vector2.Distance(containerHover.GetMousePosition(), container.Position) <= graphInfo.ContainerSize.x)
        {
            selectedContainer = container;
        }
    }

    public void OnDeselect()
    {
        IPieceContainer container = containerHover.GetClosestToMouseContainer();

        if (container.IncidentContainerInfo.IsIncident(selectedContainer))
        {
            graph.Swap(selectedContainer, container);
        }

        selectedContainer = null;
    }
}
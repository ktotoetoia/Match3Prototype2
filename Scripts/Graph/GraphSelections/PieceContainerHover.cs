using UnityEngine;
using System.Linq;
using Zenject;

public class PieceContainerHover : IPieceContainerHover
{
    [Inject] private IGraph graph;

    public IPieceContainer GetClosestToMouseContainer()
    {
        IPieceContainer closestContainer = graph.Containers.First();

        foreach (IPieceContainer container in graph.Containers)
        {
            closestContainer = CloserContainer(container, closestContainer);
        }

        return closestContainer;
    }

    private IPieceContainer CloserContainer(IPieceContainer container, IPieceContainer closestContainer)
    {
        if (IsCloserToMouse(container.Position, closestContainer.Position))
        {
            return container;
        }

        return closestContainer;
    }

    private bool IsCloserToMouse(Vector2 from, Vector2 to)
    {
        Vector2 mousePosition = GetMousePosition();

        return Vector2.Distance(from, mousePosition) < Vector2.Distance(to, mousePosition);
    }

    public Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
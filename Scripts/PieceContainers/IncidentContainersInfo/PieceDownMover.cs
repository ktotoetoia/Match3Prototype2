using UnityEngine;

public class PieceDownMover : IPieceAction
{
    private IPieceContainer container;
    private IPieceContainer downContainer;

    public PieceDownMover(IPieceContainer container)
    {
        this.container = container;
    }

    public void Update()
    {
        if (downContainer != null && container.Piece != null)
        {
            container.TryMovePieceTo(downContainer);
        }
    }

    public void AddContainer(IPieceContainer containerToAdd)
    {
        Vector2Int thisContainerPosition = container.ContainerInfo.GraphPosition;
        Vector2Int containerToAddPosition = containerToAdd.ContainerInfo.GraphPosition;

        if (thisContainerPosition.y > containerToAddPosition.y && thisContainerPosition.x == containerToAddPosition.x)
        {
            downContainer = containerToAdd;
        }
    }
}
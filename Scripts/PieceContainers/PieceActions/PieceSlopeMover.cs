using System.Collections.Generic;
using UnityEngine;

public class PieceSlopeMover : IPieceAction
{
    private IPieceContainer container;
    private IPieceContainer downContainer;
    private List<IPieceContainer> containers = new List<IPieceContainer>();

    public PieceSlopeMover(IPieceContainer container)
    {
        this.container = container;
    }

    public void Update()
    {
        if (container.IsEmpty || downContainer == null|| downContainer.IsEmpty ) return;
        
        foreach(IPieceContainer slopeContainer in containers)
        {
            if(container.TryMovePieceTo(slopeContainer))
            {
                return;
            }
        }
    }

    public void AddContainer(IPieceContainer containerToAdd)
    {
        Vector2Int thisContainerPosition = container.ContainerInfo.GraphPosition;
        Vector2Int containerToAddPosition = containerToAdd.ContainerInfo.GraphPosition;

        if(containerToAddPosition.y < thisContainerPosition.y)
        {
            if(containerToAddPosition.x != thisContainerPosition.x)
            {
                containers.Add(containerToAdd);
                return;
            }

            downContainer = containerToAdd;    
        }
    }
}
using UnityEngine;

public class GraphConnector : IGraphConnector
{
    public void ConnectContainers(IPieceContainer[,] containers)
    {
        for (int i = 0; i < containers.GetLength(0); i++)
        {
            for (int j = 0; j < containers.GetLength(1); j++)
            {
                ConnectWithNearestContainers(containers, i, j);
            }
        }
    }

    private void ConnectWithNearestContainers(IPieceContainer[,] containers, int i, int j)
    {
        TryAddIncidentContainer(containers, containers[i, j], new Vector2Int(i + 1, j));
        TryAddIncidentContainer(containers, containers[i, j], new Vector2Int(i - 1, j));
        TryAddIncidentContainer(containers, containers[i, j], new Vector2Int(i, j + 1));
        TryAddIncidentContainer(containers, containers[i, j], new Vector2Int(i, j - 1));
        TryAddIncidentContainer(containers, containers[i, j], new Vector2Int(i - 1, j - 1));
        TryAddIncidentContainer(containers, containers[i, j], new Vector2Int(i + 1, j - 1));
    }

    private void TryAddIncidentContainer(IPieceContainer[,] containers, IPieceContainer container, Vector2Int to)
    {
        if (to.x >= 0 && to.x < containers.GetLength(0) && to.y >= 0 && to.y < containers.GetLength(1))
        {
            container.IncidentContainerInfo.AddIncidentContainer(containers[to.x, to.y]);
        }
    }
}
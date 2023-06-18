using UnityEngine;

public struct ContainerInfo
{
    public bool IsReal { get; set; }
    public Vector2 WorldPosition { get; set; }
    public Vector2Int GraphPosition { get; set; }

    public ContainerInfo(Vector2 worldPosition,Vector2Int graphPosition)
    {
        IsReal = true;
        WorldPosition = worldPosition;
        GraphPosition = graphPosition;
    }
}
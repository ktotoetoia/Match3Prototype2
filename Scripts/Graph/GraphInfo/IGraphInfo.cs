using UnityEngine;

public interface IGraphInfo
{
    public Vector2 ContainerSize { get; }
    public int columns { get; }
    public int rows { get; }

    public ContainerInfo[,] ContainersGrid { get; }
}
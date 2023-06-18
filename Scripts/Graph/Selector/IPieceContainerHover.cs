using UnityEngine;

public interface IPieceContainerHover
{
    IPieceContainer GetClosestToMouseContainer();
    Vector2 GetMousePosition();
}
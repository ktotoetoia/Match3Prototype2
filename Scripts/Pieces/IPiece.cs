using UnityEngine;

public interface IPiece
{
    public bool Arrived { get;  }
    public PieceColor Color { get; }
    public void AddPosition(Vector2 position);
    public void SetPosition(Vector2 position);
    public void OnMatch();
}

public enum PieceColor
{
    red,
    yellow,
    green,
    blue,
    purple,
}
using UnityEngine;

public interface IPiece
{
    public bool Arrived { get;  }
    public PieceColor Color { get; }
    public PieceType Type { get; }
    public void AddPosition(Vector2 position);
    public void SetPosition(Vector2 position);
    public void OnMatch(IMatchInfo matchInfo);
}

public enum PieceColor
{
    red,
    yellow,
    green,
    blue,
    purple,
}

public enum PieceType
{
    Common,
    Bomb,
    Horizontal,
    Vertical,
}
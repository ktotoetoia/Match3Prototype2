using UnityEngine;

public interface IPieceContainer : IGraphUpdatable
{
    public IPiece Piece { get; }
    public Vector2 Position { get; }
    public ContainerInfo ContainerInfo { get; }
    public bool IsEmpty { get; }
    public IIncidentContainersInfo IncidentContainerInfo { get; set; }
    public bool TryConnect(IPiece piece);
    public IPiece ChangePiece(IPiece piece);
    public IPiece Disconnect();
    public bool TryMovePieceTo(IPieceContainer container);
    public void OnMatch(IMatchInfo matchInfo);
    public void TransformToPiece(IMatchInfo matchInfo, IPiece piece);
    void Connect(IPiece piece);
}
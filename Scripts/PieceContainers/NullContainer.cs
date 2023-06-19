using UnityEngine;

public class NullContainer : IPieceContainer
{
    public IPiece Piece { get; }
    public Vector2 Position { get { return Vector2.positiveInfinity; } }
    public bool IsEmpty { get; }
    public ContainerInfo ContainerInfo { get; private set; }
    public IIncidentContainersInfo IncidentContainerInfo { get; set; }

    public NullContainer()
    {
        IncidentContainerInfo = new IncidentContainersInfo(this);
    }
    
    public IPiece Disconnect()
    {
        return null;
    }

    public bool TryConnect(IPiece piece)
    {
        return false;
    }

    public bool TryMovePieceTo(IPieceContainer container)
    {
        return false;
    }

    public void Update()
    {

    }

    public IPiece ChangePiece(IPiece piece)
    {
        return null;
    }

    public void OnMatch()
    {

    }
}
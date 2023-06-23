using UnityEngine;

public class PieceContainer : IPieceContainer
{
    public IPiece Piece { get; private set; }
    public bool IsEmpty { get { return Piece == null; } }
    public Vector2 Position { get { return ContainerInfo.WorldPosition; } }
    public ContainerInfo ContainerInfo { get; private set; }
    public IIncidentContainersInfo IncidentContainerInfo { get; set; }
    
    public PieceContainer(ContainerInfo containerInfo)
    {
        ContainerInfo = containerInfo;
    }

    public bool TryConnect(IPiece piece)
    {
        if (IsEmpty)
        {
            Connect(piece);
            return true;
        }

        return false;
    }

    public void Connect(IPiece piece)
    {
        SetPiece(piece);
        Update();
    }

    private void SetPiece(IPiece piece)
    {
        Piece = piece;
        piece?.AddPosition(Position);
    }

    public IPiece Disconnect()
    {
        IPiece piece = Piece;
        Piece = null;
        return piece;
    }

    public bool TryMovePieceTo(IPieceContainer container)
    {
        if (container.IsEmpty)
        {
            container.Connect(Disconnect());
            Update();
            return true;
        }
        
        return false;
    }

    public IPiece ChangePiece(IPiece piece)
    {
        IPiece disconnected = Piece;

        Connect(piece);

        return disconnected;
    }

    public void OnMatch(IMatchInfo matchInfo)
    {
        Piece?.OnMatch(matchInfo,this);
        Disconnect();
    }

    public void Update()
    {
        if (!IsEmpty)
        {
            IncidentContainerInfo.UpdateActions();
        }

        IncidentContainerInfo.Update();
    }

    public void TransformToPiece(IMatchInfo matchInfo, IPiece piece)
    {
        Piece?.OnMatch(matchInfo,this);
        SetPiece(piece);
    }
}
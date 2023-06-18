using System.Collections.Generic;

public class Column : IColumn
{
    public bool IsEmpty { get { return containers[0].IsEmpty; } }

    private List<IPieceContainer> containers = new List<IPieceContainer>();
    public IEnumerable<IPieceContainer> Containers
    {
        get
        {
            foreach(IPieceContainer container in containers) yield return container;
        }
    }

    public Column(List<IPieceContainer> containers)
    {
        this.containers = containers;
    }

    public void AddToTop(IPiece piece)
    {
        piece.SetPosition(containers[0].Position);
        containers[0].TryConnect(piece);
    }
}
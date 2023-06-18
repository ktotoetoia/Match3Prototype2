using System.Collections.Generic;

public interface IColumn
{
    public bool IsEmpty { get; }
    public IEnumerable<IPieceContainer> Containers { get; }
    public void AddToTop(IPiece piece);
}
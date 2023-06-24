using System.Collections.Generic;

public interface IGraph
{
    public IEnumerable<IColumn> Columns { get; }
    public IEnumerable<IPieceContainer> Containers { get; }
    public void AddToColumn(IPiece piece,IColumn column);
    public void Swap(IPieceContainer from, IPieceContainer to);
}
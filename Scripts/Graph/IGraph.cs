using System.Collections.Generic;

public interface IGraph : IGraphUpdatable
{
    public IEnumerable<IColumn> Columns { get; }
    public IEnumerable<IPieceContainer> Containers { get; }
    public void AddToColumn(IPiece piece,IColumn column);
    public void AddGraphUpdatable(IGraphUpdatable updatable);
    public void Swap(IPieceContainer from, IPieceContainer to);
}
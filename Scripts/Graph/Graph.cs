using System.Collections.Generic;
using Zenject;

public class Graph : IGraph
{
    [Inject] private IMatchChecker matchChecker;
    
    private IPieceContainer[,] containers;
    private List<IColumn> columns = new List<IColumn>();

    private List<IGraphUpdatable> graphUpdatables = new List<IGraphUpdatable>();

    public IEnumerable<IPieceContainer> Containers
    {
        get
        {
            foreach(IPieceContainer container in containers) yield return container;
        }
    }

    public IEnumerable<IColumn> Columns
    {
        get
        {
            foreach (IColumn column in columns) yield return column;
        }
    }

    public Graph(IGraphCreator graphCreator)
    {
        containers = graphCreator.CreateContainers();
        columns = graphCreator.CreateColumns(containers);
    }

    public void Update()
    {
        foreach(IGraphUpdatable updatable in graphUpdatables)
        {
            updatable.Update();
        }
    }

    public void AddToColumn(IPiece piece,IColumn column)
    {
        column.AddToTop(piece);
    }

    public void AddGraphUpdatable(IGraphUpdatable updatable)
    {
        graphUpdatables.Add(updatable);
    }

    public void Swap(IPieceContainer from, IPieceContainer to)
    {
        from.ChangePiece(to.ChangePiece(from.Piece));

        matchChecker.CheckMatch(to,from);
    }
}
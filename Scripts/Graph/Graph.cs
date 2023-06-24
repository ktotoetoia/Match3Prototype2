using System.Collections.Generic;
using Zenject;
using UnityEngine;

public class Graph : MonoBehaviour, IGraph
{
    [Inject] private IMatchChecker matchChecker;
    [Inject] private IGraphCreator graphCreator;

    private IPieceContainer[,] containers;
    private List<IColumn> columns = new List<IColumn>();

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
    
    private void Awake()
    {
        containers = graphCreator.CreateContainers();
        columns = graphCreator.CreateColumns(containers);
    }

    public void AddToColumn(IPiece piece,IColumn column)
    {
        column.AddToTop(piece);
    }

    public void Swap(IPieceContainer from, IPieceContainer to)
    {
        from.ChangePiece(to.ChangePiece(from.Piece));

        matchChecker.CheckMatch(to,from);
    }
}
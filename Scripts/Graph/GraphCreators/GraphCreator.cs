using System.Collections.Generic;
using Zenject;

public class GraphCreator : IGraphCreator
{
    private IGraphInfo graphInfo;
    private IGraphConnector graphConnector;

    private IContainerFactory containerFactory;
    private IContainerFactory nullContainerFactory;
    private IColumnFactory columnFactory;

    [Inject]
    public GraphCreator(IContainerFactory containerFactory,IColumnFactory columnFactory,IGraphInfo graphInfo)
    {
        this.graphInfo = graphInfo;
        this.containerFactory = containerFactory;
        this.columnFactory = columnFactory;

        nullContainerFactory = new NullContainerFactory();
        graphConnector = new GraphConnector();
    }

    public IPieceContainer[,] CreateContainers()
    {
        ContainerInfo[,] grid = graphInfo.ContainersGrid;
        
        int length0 = grid.GetLength(0);
        int length1 = grid.GetLength(1);

        IPieceContainer[,] containers = new IPieceContainer[length0,length1];

        for (int i = 0; i < length0; i++)
        {
            for (int j = 0; j < length1; j++)
            {
                containers[i, j] = GetInstanceOfContainer(grid[i,j]);
            }
        }

        graphConnector.ConnectContainers(containers);
        return containers;
    }

    private IPieceContainer GetInstanceOfContainer(ContainerInfo containerInfo)
    {
        if (!containerInfo.IsReal)
        {
            return nullContainerFactory.Create(containerInfo);
        }

        return containerFactory.Create(containerInfo);
    }

    public List<IColumn> CreateColumns(IPieceContainer[,] containers)
    {
        List<IColumn> columns = new List<IColumn>();

        for (int i = 0; i < containers.GetLength(0); i++)
        {
            columns.Add(columnFactory.Create(GetColumnContainers(containers,i)));
        }

        return columns;
    }

    private List<IPieceContainer> GetColumnContainers(IPieceContainer[,] containers,int columnIndex)
    {
        List<IPieceContainer> columnContainers = new List<IPieceContainer>();

        for (int j = containers.GetLength(1)-1; j >= 0; j--)
        {
            columnContainers.Add(containers[columnIndex, j]);
        }

        return columnContainers;
    }
}
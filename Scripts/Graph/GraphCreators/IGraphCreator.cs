using System.Collections.Generic;

public interface IGraphCreator
{
    public IPieceContainer[,] CreateContainers();
    public List<IColumn> CreateColumns(IPieceContainer[,] containers);
}

public interface IGraphConnector
{
    public void ConnectContainers(IPieceContainer[,] containers);
}
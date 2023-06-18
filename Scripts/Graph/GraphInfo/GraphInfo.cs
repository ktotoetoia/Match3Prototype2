using UnityEngine;

public class GraphInfo : IGraphInfo
{
    public Vector2 ContainerSize { get; private set; } = Vector2.one; 
    public int columns { get; set; }
    public int rows { get; set; }

    public ContainerInfo[,] ContainersGrid { get; set; }

    private Vector2 offset = new Vector2(0.5f, 0.5f);

    public GraphInfo(int columns, int rows)
    {
        this.columns = columns;
        this.rows = rows;

        InitializeGrid();
    }

    private void InitializeGrid()
    {
        ContainersGrid = new ContainerInfo[columns, rows];

        for (int i = 0; i < ContainersGrid.GetLength(0); i++)
        {
            for (int j = 0; j < ContainersGrid.GetLength(1); j++)
            {
                if (i == 0 && j == 3 || i == 9 && j == 3 || i == 1 && j == 3 || i == 8 && j == 3)
                {
                    continue;
                }
    
                ContainersGrid[i, j] = new ContainerInfo(GetPosition(new Vector2(i,j)),new Vector2Int(i,j));
            }
        }
    }

    private Vector2 GetPosition(Vector2 indexes)
    {
        float x = ContainerSize.x * indexes.x - columns * ContainerSize.x / 2;
        float y = ContainerSize.y * indexes.y - rows * ContainerSize.y / 2;

        return new Vector2(x, y) + offset;
    }
}
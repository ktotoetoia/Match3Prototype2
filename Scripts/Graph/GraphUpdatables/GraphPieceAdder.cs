using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class GraphPieceAdder : MonoBehaviour
{
    [SerializeField] private float rowPieceAddingDelay = 0.4f;
    [SerializeField] private List<GameObject> pieces;

    [Inject] private IGraph graph;

    private float lastSpawnTime;

    void Update()
    {
        if(lastSpawnTime + rowPieceAddingDelay <= Time.time)
        {
            foreach (IColumn column in graph.Columns)
            {
                if (column.IsEmpty)
                {
                    AddRandomPieceToColumn(column);
                }
            }

            lastSpawnTime = Time.time;
        }
    }

    private IPiece AddRandomPieceToColumn(IColumn column)
    {
        IPiece piece = Instantiate(pieces[Random.Range(0, pieces.Count)]).GetComponent<IPiece>();

        graph.AddToColumn(piece,column);
        
        return piece;
    }
}
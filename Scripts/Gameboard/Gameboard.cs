using UnityEngine;
using Zenject;
using System.Collections.Generic;
using System.Linq;

public class Gameboard : MonoBehaviour
{
    [SerializeField] private List<Component> graphUpdatables;
    [Inject] private IGraph graph;

    private void Start()
    {
        AddUpdatablesToGraph();
    }

    private void FixedUpdate()
    {
        graph.Update();
    }

    private void AddUpdatablesToGraph()
    {
        foreach (IGraphUpdatable graphUpdatable in graphUpdatables
            .Select(x => x.GetComponent<IGraphUpdatable>()))
        {
            if(graphUpdatable == null)
            {
                Debug.LogWarning("Component is not implements IGraphUpdatable");
                continue;
            }
            
            graph.AddGraphUpdatable(graphUpdatable);
        }
    }
}
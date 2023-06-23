using System.Collections;
using UnityEngine;
using Zenject;

public class ContainersUpdater : MonoBehaviour
{
    [Inject] private IGraph graph;

    private void Start()
    {
        foreach(IPieceContainer container in graph.Containers)
        {
            container.IncidentContainerInfo.ContainerActionsUpdated += LateContainerInfoUpdate; 
        }
    }

    private void LateContainerInfoUpdate(IIncidentContainersInfo incidentContainerInfo)
    {
        StartCoroutine(LateUpdateAtTheEndOfFrame(incidentContainerInfo));
    }

    private IEnumerator LateUpdateAtTheEndOfFrame(IIncidentContainersInfo incidentContainerInfo)
    {
        yield return new WaitForEndOfFrame();
        incidentContainerInfo.LateUpdateActions();
    }
}
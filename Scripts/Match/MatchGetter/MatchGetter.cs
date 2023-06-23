using System.Collections.Generic;
using System.Linq;

public delegate IPieceContainer GetNextContainer(IPieceContainer container);

public class MatchGetter : IMatchGetter
{
    public List<IPieceContainer> GetSquareMatch(IPieceContainer center, int radius)
    {
        List<IPieceContainer> containers = GetVerticalInRadius(center, radius);

        foreach(IPieceContainer container in new List<IPieceContainer>(containers))
        {
            containers.AddRange(GetHorizontalInRadius(container, radius));
        }
        
        return containers.Distinct().Where(x=>x!= null).ToList();
    }

    private List<IPieceContainer> GetHorizontalInRadius(IPieceContainer container,  int radius)
    {
        List<IPieceContainer> containers = new List<IPieceContainer>()
        {
            container,
        };
        
        GetContainersInRadius(container, containers, GetLeftContainer, radius);
        GetContainersInRadius(container, containers, GetRightContainer, radius);

        return containers;
    }

    private List<IPieceContainer> GetVerticalInRadius(IPieceContainer container, int radius)
    {
        List<IPieceContainer> containers = new List<IPieceContainer>()
        {
            container,
        };
        
        GetContainersInRadius(container, containers, GetUpContainer, radius);
        GetContainersInRadius(container, containers, GetDownContainer, radius);
        
        return containers;
    }

    public List<IPieceContainer> GetVerticalMatchs(IPieceContainer container,  bool anyColor = false)
    {
        List<IPieceContainer> containers = new List<IPieceContainer>()
        {
            container,
        };

        GetContainers(container, containers, GetUpContainer, anyColor);
        GetContainers(container, containers, GetDownContainer, anyColor);

        return containers;
    }

    public List<IPieceContainer> GetHorizontalMatchs(IPieceContainer container, bool anyColor = false)
    {
        List<IPieceContainer> containers = new List<IPieceContainer>()
        {
            container,
        };

        GetContainers(container, containers, GetLeftContainer, anyColor);
        GetContainers(container, containers, GetRightContainer, anyColor);

        return containers;
    }

    private IPieceContainer GetUpContainer(IPieceContainer container)
    {
        return container?.IncidentContainerInfo.Up;
    }

    private IPieceContainer GetDownContainer(IPieceContainer container)
    {
        return container?.IncidentContainerInfo.Down;
    }

    private IPieceContainer GetLeftContainer(IPieceContainer container)
    {
        return container?.IncidentContainerInfo.Left;
    }

    private IPieceContainer GetRightContainer(IPieceContainer container)
    {
        return container?.IncidentContainerInfo.Right;
    }

    private void GetContainers(IPieceContainer container, List<IPieceContainer> containers, GetNextContainer getNextContainer, bool anyColor = false)
    {
        IPieceContainer nextContainer = getNextContainer(container);

        if (container != null && (anyColor || nextContainer?.Piece?.Color == container?.Piece?.Color))
        {
            containers.Add(nextContainer);
            GetContainers(nextContainer, containers, getNextContainer,anyColor);
        }
    }

    public void GetContainersInRadius(IPieceContainer container, List<IPieceContainer> containers, GetNextContainer getNextContainer,int range)
    {
        IPieceContainer nextContainer = getNextContainer(container);

        if (container != null && range > 0)
        {
            containers.Add(nextContainer);
         
            GetContainersInRadius(nextContainer, containers, getNextContainer,range-1);
        }
    }
}
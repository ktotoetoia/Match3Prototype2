using Zenject;

public class IncidentContainersInfoFactory : IFactory<IPieceContainer,IIncidentContainersInfo>
{
    public IIncidentContainersInfo Create(IPieceContainer container)
    {
        IIncidentContainersInfo IncidentContainerInfo = new IncidentContainersInfo(container);

        IncidentContainerInfo.AddAction(new PieceDownMover(container));
        IncidentContainerInfo.AddLateAction(new PieceSlopeMover(container));

        return IncidentContainerInfo;
    }
}
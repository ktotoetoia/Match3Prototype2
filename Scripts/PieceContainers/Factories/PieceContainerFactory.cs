using Zenject;

public class PieceContainerFactory : IContainerFactory
{
    private IFactory<IPieceContainer, IIncidentContainersInfo> incidentContainerInfoFactory = new IncidentContainersInfoFactory();

    public IPieceContainer Create(ContainerInfo info)
    {
        IPieceContainer container = new PieceContainer(info);
        container.IncidentContainerInfo = incidentContainerInfoFactory.Create(container);
        return container;
    }
}
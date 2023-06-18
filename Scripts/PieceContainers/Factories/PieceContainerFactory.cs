public class PieceContainerFactory : IContainerFactory
{
    public IPieceContainer Create(ContainerInfo info)
    {
        return new PieceContainer(info);
    }
}
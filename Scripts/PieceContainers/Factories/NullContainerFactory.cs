using UnityEngine;

public class NullContainerFactory : IContainerFactory
{
    public IPieceContainer Create(ContainerInfo containerInfo)
    {
        return new NullContainer();
    }
}
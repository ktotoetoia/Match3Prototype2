using UnityEngine;

public interface IContainerFactory
{
    IPieceContainer Create(ContainerInfo containerInfo);
}
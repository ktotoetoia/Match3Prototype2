using Zenject;

public class SelectorInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IPieceContainerHover>().To<PieceContainerHover>().AsSingle();
        Container.Bind<IPieceContainerMover>().To<PieceContainerMover>().AsSingle();
    }
}
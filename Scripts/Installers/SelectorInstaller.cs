using Zenject;

public class SelectorInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IMatcher>().To<Matcher>().AsSingle();
        Container.Bind<IMatchGetter>().To<MatchGetter>().AsSingle();
        Container.Bind<IPieceInstantiator>().To<PieceInstantiator>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IMatchTransformator>().To<MatchTransformator>().AsSingle();
        Container.Bind<IPieceContainerHover>().To<PieceContainerHover>().AsSingle();
        Container.Bind<IPieceContainerMover>().To<PieceContainerMover>().AsSingle();
    }
}
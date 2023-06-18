using Zenject;


public class GraphInstaller : MonoInstaller
{
    [Inject] private LevelSettings levelSettings;

    public override void InstallBindings()
    {
        Container.Bind<IMatchChecker>().To<MatchChecker>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IGraphInfo>().FromInstance(levelSettings.GraphInfo);
        Container.Bind<IContainerFactory>().To<PieceContainerFactory>().AsTransient();
        Container.Bind<IColumnFactory>().To<ColumnFactory>().AsTransient().WithConcreteId(null);
        Container.Bind<IGraphCreator>().To<GraphCreator>().AsTransient();
        Container.Bind<IGraph>().To<Graph>().AsSingle();
    }
}
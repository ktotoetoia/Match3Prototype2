using Zenject;
using UnityEngine;

public class LevelSettingsInstaller : MonoInstaller
{
    [SerializeField] LevelSettings levelSettings;

    public override void InstallBindings()
    {
        Container.Bind<LevelSettings>().FromInstance(levelSettings);
    }
}
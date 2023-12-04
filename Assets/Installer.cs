using Ginox.Pain.Weapon.Scripts;
using Ginox.Pain.Weapon.UI;
using Zenject;

public class Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<WeaponController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Crosshair>().FromComponentInHierarchy().AsSingle();
    }
}

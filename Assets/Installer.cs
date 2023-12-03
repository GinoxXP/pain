using Ginox.Pain.Weapon.Scripts;
using Zenject;

public class Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<WeaponController>().FromComponentInHierarchy().AsSingle();
    }
}

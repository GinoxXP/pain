namespace Ginox.Pain.Weapon.Scripts
{
    public interface IWeapon
    {
        /// <summary>
        /// Activate when player push down attack button.
        /// </summary>
        void TriggerDown();

        /// <summary>
        /// Activate when plaer push up attack button.
        /// </summary>
        void TriggerUp();
    }
}

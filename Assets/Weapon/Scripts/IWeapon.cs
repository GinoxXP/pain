using UnityEngine;

namespace Ginox.Pain.Weapon
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

        /// <summary>
        /// Reload weapon.
        /// </summary>
        void Reload();

        /// <summary>
        /// Set layer mask for weapon.
        /// </summary>
        void SetLayerMask(LayerMask layerMask);

        /// <summary>
        /// Change rigs to aim state.
        /// </summary>
        void Aim();

        /// <summary>
        /// Change rigs to idle state.
        /// </summary>
        void Idle();
    }
}

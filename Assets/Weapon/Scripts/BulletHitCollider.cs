using System;
using UnityEngine;

namespace Ginox.Pain.Weapon.Scripts
{
    public class BulletHitCollider : MonoBehaviour, IBulletHit
    {
        public event Action Hited;

        public void Hit()
        {
            Hited?.Invoke();
        }
    }
}

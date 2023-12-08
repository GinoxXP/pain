using System;
using UnityEngine;

namespace Ginox.Pain.Weapon
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

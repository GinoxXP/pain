using System;
using UnityEngine;

public class BulletHitCollider : MonoBehaviour, IBulletHit
{
    public event Action Hited;

    public void Hit()
    {
        Hited?.Invoke();
    }
}

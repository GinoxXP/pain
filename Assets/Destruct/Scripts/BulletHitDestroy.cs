
using Ginox.Pain.Weapon;
using UnityEngine;

namespace Ginox.Pain.Destruct
{
    public class BulletHitDestroy : MonoBehaviour
    {
        private MeshDestroy meshDestroy;
        private BulletHitCollider bulletHitCollider;

        private void OnHited()
        {
            meshDestroy.DestroyMesh();
        }

        private void Start()
        {
            meshDestroy = GetComponent<MeshDestroy>();
            bulletHitCollider = GetComponent<BulletHitCollider>();

            bulletHitCollider.Hited += OnHited;
        }

        private void OnDestroy()
        {
            bulletHitCollider.Hited -= OnHited;
        }
    }
}

using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    private new Camera camera;

    public void Shot()
    {
        var targetPosition = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camera.nearClipPlane));
        Shot(targetPosition);
    }

    private void Shot(Vector3 targetPosition)
    {
        var direction = targetPosition - camera.transform.position;
        Debug.DrawRay(camera.transform.position, direction, Color.red, 2);
        if (Physics.Raycast(new Ray(camera.transform.position, direction), out var hit, float.PositiveInfinity, layerMask))
        {
            if (hit.transform.TryGetComponent<IBulletHit>(out var iBulletHit))
            {
                iBulletHit.Hit();
            }
        }
    }

    private void Start()
    {
        camera = Camera.main;
    }
}

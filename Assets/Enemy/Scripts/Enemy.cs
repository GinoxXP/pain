using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private BulletHitCollider bulletHitCollider;

    private NavMeshAgent agent;

    public void Move(Vector3 position)
    {
        agent.SetDestination(position);
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        bulletHitCollider.Hited += OnHited;
    }

    private void OnHited()
    {
        Debug.Log("Hit");
    }
}

using Ginox.Pain.Enemy;
using Ginox.Pain.Weapon.Scripts;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private BulletHitCollider bulletHitCollider;

    private FieldView fieldView;
    private NavMeshAgent agent;

    public void Move(Vector3 position)
    {
        agent.SetDestination(position);
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        fieldView = GetComponentInChildren<FieldView>();
        bulletHitCollider.Hited += OnHited;
        fieldView.PlayerDetected += OnPlayerDetected;
        fieldView.PlayerLost += OnPlayerLost;
    }

    private void OnDestroy()
    {
        bulletHitCollider.Hited -= OnHited;
        fieldView.PlayerDetected -= OnPlayerDetected;
        fieldView.PlayerLost -= OnPlayerLost;
    }

    private void OnPlayerLost()
    {
        Move(fieldView.LastPlayerPosition);
    }

    private void OnPlayerDetected()
    {
        Debug.Log("I see you");
    }

    private void OnHited()
    {
        Debug.Log("Hit");
    }
}

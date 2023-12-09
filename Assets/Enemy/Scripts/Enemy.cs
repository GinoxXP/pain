using Ginox.Pain.Enemy.Behaviours;
using Ginox.Pain.Weapon;
using UnityEngine;
using UnityEngine.AI;

namespace Ginox.Pain.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private BulletHitCollider bulletHitCollider;

        private FieldView fieldView;
        private NavMeshAgent agent;

        public IEnemyBehaviour EnemyBehaviour { get; set; }

        public void Move(Vector3 position)
        {
            agent.SetDestination(position);
        }

        private void OnHited()
        {

        }

        private void Update()
        {
            EnemyBehaviour?.Update();
        }

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            fieldView = GetComponentInChildren<FieldView>();

            bulletHitCollider.Hited += OnHited;

            EnemyBehaviour = new GuardBehaviour(this, fieldView, 15);
        }

        private void OnDestroy()
        {
            bulletHitCollider.Hited -= OnHited;
        }
    }
}

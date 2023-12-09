using System.Threading.Tasks;
using UnityEngine;

namespace Ginox.Pain.Enemy.Behaviours
{
    public class GuardBehaviour : IEnemyBehaviour
    {
        private readonly int maxGuardDistance;
        private readonly Enemy enemy;
        private readonly FieldView fieldView;
        private readonly Vector3 guardPosition;

        private bool isMovingToGuardDistance;
        private bool isPlayerDetected;

        public GuardBehaviour(Enemy enemy, FieldView fieldView, int maxGuardDistance)
        {
            this.enemy = enemy;
            this.maxGuardDistance = maxGuardDistance;
            this.fieldView = fieldView;
            guardPosition = enemy.transform.position;

            fieldView.PlayerDetected += OnPlayerDetected;
            fieldView.PlayerLost += OnPlayerLost;
        }

        public async void Update()
        {
            var isSoFar = Vector3.Distance(enemy.transform.position, guardPosition) > maxGuardDistance;

            if (isPlayerDetected)
            {
                enemy.Move(fieldView.LastPlayerPosition);
            }
            else if (!isMovingToGuardDistance)
            {
                await Task.Delay(5000);
                isMovingToGuardDistance = true;
            }

            if (isSoFar)
            {
                isMovingToGuardDistance = true;
                await Task.Delay(5000);
                enemy.Move(guardPosition);
            }
        }

        private void OnPlayerDetected()
            => isPlayerDetected = true;

        private void OnPlayerLost()
            => isPlayerDetected = false;
    }
}

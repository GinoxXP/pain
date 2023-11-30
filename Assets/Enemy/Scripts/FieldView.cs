using System;
using UnityEngine;

namespace Ginox.Pain.Enemy
{
    public class FieldView : MonoBehaviour
    {
        public event Action PlayerDetected;
        public event Action PlayerLost;

        private Player.Scripts.Player player;

        public Vector3? LastPlayerPosition => player == null ? null : player.transform.position;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player.Scripts.Player>(out var player))
            {
                this.player = player;
                PlayerDetected?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Player.Scripts.Player>(out _))
            {
                this.player = null;
                PlayerLost?.Invoke();
            }
        }
    }
}

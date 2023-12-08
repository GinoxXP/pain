using System;
using UnityEngine;

namespace Ginox.Pain.Enemy
{
    public class FieldView : MonoBehaviour
    {
        public event Action PlayerDetected;
        public event Action PlayerLost;

        private Player.Player player;

        public Vector3 LastPlayerPosition { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out var player))
            {
                if (this.player != null)
                    return;

                this.player = player;
                PlayerDetected?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out _))
            {
                if (player == null)
                    return;

                LastPlayerPosition = player.transform.position;
                player = null;
                PlayerLost?.Invoke();
            }
        }
    }
}

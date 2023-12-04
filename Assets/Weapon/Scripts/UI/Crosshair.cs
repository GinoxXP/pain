using UnityEngine;

namespace Ginox.Pain.Weapon.UI
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField]
        private GameObject crosshair;

        public bool Enabled { get => crosshair.activeSelf; set => crosshair.SetActive(value); }

        private void Start ()
        {
            Enabled = false;
        }
    }
}

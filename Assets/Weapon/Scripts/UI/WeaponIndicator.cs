using UnityEngine;

namespace Ginox.Pain.Weapon.UI
{
    public class WeaponIndicator : MonoBehaviour
    {
        [SerializeField]
        private GameObject selected;

        public bool Selected
        {
            get => selected.activeSelf;
            set => selected.SetActive(value);
        }
    }
}

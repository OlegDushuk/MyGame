using UnityEngine;

namespace Gameplay
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private KeyCode inventoryKey;

        public bool GetInventoryKeyDown()
        {
            return Input.GetKeyDown(inventoryKey);
        }
    }
}

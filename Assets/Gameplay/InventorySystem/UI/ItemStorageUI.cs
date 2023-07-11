using UnityEngine;

namespace Gameplay.InventorySystem.UI
{
    public class ItemStorageUI : MonoBehaviour
    {
        [SerializeField] private SlotUI prefabSlotUI;
        
        public void UpdateUI(ItemStorage itemStorage, int amountSlots)
        {
            if(!prefabSlotUI) return;
            
            foreach (Transform slot in transform)
            {
                Destroy(slot.gameObject);
            }
            
            for (var i = 0; i < amountSlots; i++)
            {
                var slot = Instantiate(prefabSlotUI, transform);
                slot.UpdateSlot(itemStorage, i);
            }
        }
    }
}
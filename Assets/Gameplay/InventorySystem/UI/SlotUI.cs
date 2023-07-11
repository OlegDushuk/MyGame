using UnityEngine;

namespace Gameplay.InventorySystem.UI
{
    public class SlotUI : MonoBehaviour
    {
        [SerializeField] private ItemView prefabItemView;
        
        private ItemStorage _itemStorage;
        private int _index;
        
        public void UpdateSlot(ItemStorage itemStorage, int index)
        {
            _itemStorage = itemStorage;
            
            if (index < 0)
            {
                _index = 0;
                return;
            }
            
            _index = index;
            
            itemStorage.OnInventoryUpdate += OnChangesItem;
        }
        
        private void OnChangesItem(int index, Item item)
        {
            if(index != _index) return;
            
            
        }
    }
}
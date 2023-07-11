using Gameplay.InventorySystem.UI;

namespace Gameplay.InventorySystem
{
    public delegate void InventoryUpdateHandler(int index, Item item);
    
    public class ItemStorage
    {
        private readonly Item[] _items;
        
        public event InventoryUpdateHandler OnInventoryUpdate;
        
        public ItemStorage(int amountSlots = 1)
        {
            if (amountSlots <= 0)
                amountSlots = 1;
            
            _items = new Item[amountSlots];
        }
        
        private bool IsValidItem(int index)
        {
            return index >= 0 && index < _items.Length;
        }
        
        private bool HasItem(int index)
        {
            return IsValidItem(index) && _items[index] != null;
        }
        
        private bool IsStackableItem(int index)
        {
            return HasItem(index) && _items[index] is StackableItem;
        }
        
        private int FindEmptySlotIndex(int startIndex = 0)
        {
            for (var i = startIndex; i < _items.Length; i++)
                if (!HasItem(i))
                    return i;
            return -1;
        }
        
        public void ConnectUI(ItemStorageUI itemStorageUI)
        {
            itemStorageUI.UpdateUI(this, _items.Length);
        }
    }
}
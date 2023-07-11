namespace Gameplay.InventorySystem
{
    public class Item
    {
        public ItemData ItemData { get; private set; }
        
        public Item(ItemData itemData) => ItemData = itemData;
    }
}
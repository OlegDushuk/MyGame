using UnityEngine;

namespace Gameplay.InventorySystem
{
    public class StackableItemData : ItemData
    {
        [SerializeField] private int maxAmount;
        
        public int MaxAmount => maxAmount;
        
        public override Item CreateItem()
        {
            return new StackableItem(this);
        }
    }
}
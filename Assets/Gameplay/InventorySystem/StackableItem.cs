using UnityEngine;

namespace Gameplay.InventorySystem
{
    public class StackableItem : Item
    {
        public StackableItemData StackableItemData { get; private set; }
        public int Amount { get; private set; }
        
        public int MaxAmount => StackableItemData.MaxAmount;
        public bool IsFull => Amount == MaxAmount;
        public bool IsEmpty => Amount == 0;
        
        public StackableItem(StackableItemData itemData, int amount = 1) : base(itemData)
        {
            StackableItemData = itemData;
            SetAmount(amount);
        }

        public void SetAmount(int amount)
        {
            Amount = Mathf.Clamp(amount, 0, MaxAmount);
        }
        
        public int AddAmountAndGetExcess(int amount)
        {
            var nextAmount = Amount + amount;
            SetAmount(nextAmount);

            return (nextAmount > MaxAmount) ? (nextAmount - MaxAmount) : 0;
        }
        
        public StackableItem SeparateAndClone(int amount)
        {
            if(Amount <= 1) return null;

            if(amount > Amount - 1)
                amount = Amount - 1;
        
            Amount -= amount;
            return new StackableItem(StackableItemData, amount);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.InventorySystem
{
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private Image itemIcon;
        
        public string Name => itemName;
        public Image Icon => itemIcon;

        public virtual Item CreateItem()
        {
            return new Item(this);
        }
    }
}
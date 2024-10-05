using UnityEngine;

namespace EndlessRunner.Inventories
{
    public abstract class InventoryItem : ScriptableObject
    {
        [SerializeField] Sprite icon;

        public Sprite GetIcon()
        {
            return icon;
        }
    }
}

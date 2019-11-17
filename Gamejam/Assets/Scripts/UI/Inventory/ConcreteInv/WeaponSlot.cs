using System;
using UnityEngine.Events;

namespace UI.Inventory.ConcreteInv
{
    public class WeaponSlot : InventorySlot
    {
        public event Action<ArtifactItem> OnAddItem = delegate { };
        public event Action<ArtifactItem> OnRemoveItem = delegate { };

        public override bool AddItem(DraggableItem draggableItem)
        {
            if (draggableItem is ArtifactItem && (((ArtifactItem)draggableItem).Item is WeaponScriptableObject))
            {
                if (currentItem != null)
                {
                    OnRemoveItem((ArtifactItem) currentItem);
                    Destroy(currentItem.gameObject);
                }

                currentItem = draggableItem;
                draggableItem.transform.SetParent(transform);
                draggableItem.transform.position = transform.position;
                OnAddItem((ArtifactItem) currentItem);
                ((ArtifactItem) currentItem).IsLocked = true;
                return true;
            }

            return false;
        }

        public override void RemoveItem(DraggableItem item)
        {
            base.RemoveItem(item);
        }

        public override void ReplaceItem(DraggableItem item)
        {
            base.ReplaceItem(item);
            ((ArtifactItem)currentItem).IsLocked = true;
        }
    }
}

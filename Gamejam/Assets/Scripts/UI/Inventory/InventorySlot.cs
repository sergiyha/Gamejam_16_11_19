using System.Collections;
using System.Collections.Generic;
using UI.Inventory;
using UnityEngine;

public class InventorySlot : MonoBehaviour, IItemHolder
{
    protected DraggableItem currentItem;

    public DraggableItem Item => currentItem;

    public IEnumerable<DraggableItem> Items => new DraggableItem[] {currentItem};

    public virtual bool AddItem(DraggableItem draggableItem)
    {
        if (currentItem != null)
            return false;

        currentItem = draggableItem;
        draggableItem.transform.SetParent(transform);
        draggableItem.transform.position = transform.position;
        return true;
    }

    public virtual void RemoveItem(DraggableItem item)
    {
        if (item == currentItem)
            currentItem = null;
    }

    public virtual void ReplaceItem(DraggableItem item)
    {
        if (currentItem != null)
        {
            Destroy(currentItem.gameObject);
            currentItem = null;
        }

        AddItem(item);
    }
}

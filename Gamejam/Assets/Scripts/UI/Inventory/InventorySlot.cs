using System.Collections;
using System.Collections.Generic;
using UI.Inventory;
using UnityEngine;

public class InventorySlot : MonoBehaviour, IItemHolder
{
    private DraggableItem currentItem;

    public DraggableItem Item => currentItem;

    public IEnumerable<DraggableItem> Items => new DraggableItem[] {currentItem};

    public bool AddItem(DraggableItem draggableItem)
    {
        if (currentItem != null)
            return false;

        currentItem = draggableItem;
        draggableItem.transform.SetParent(transform);
        draggableItem.transform.position = transform.position;
        return true;
    }

    public void RemoveItem(DraggableItem item)
    {
        if (item == currentItem)
            currentItem = null;
    }
}

using System.Collections.Generic;
using UI.Inventory;
using UnityEngine;

public class Container : MonoBehaviour, IItemHolder
{
    [SerializeField]
    protected Transform holder;
    [SerializeField]
    private int maxCount = -1;

    private List<DraggableItem> items = new List<DraggableItem>();

    public DraggableItem Item => items.Count >= 1 ? items[0] : null;

    public IEnumerable<DraggableItem> Items => items;

    public virtual bool AddItem(DraggableItem item)
    {
        if(maxCount > 0)
            if (items.Count >= maxCount)
                return false;

        item.transform.SetParent(holder);
        return true;
    }

    public virtual void RemoveItem(DraggableItem item)
    {
        if (items.Contains(item))
            items.Remove(item);
    }
}

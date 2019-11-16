﻿using System.Collections.Generic;
using UI.Inventory;
using UnityEngine;

public class Container : MonoBehaviour, IItemHolder
{
    [SerializeField]
    private Transform holder;
    [SerializeField]
    private int maxCount = -1;

    private List<DraggableItem> items = new List<DraggableItem>();

    public DraggableItem Item => items.Count >= 1 ? items[0] : null;

    public IEnumerable<DraggableItem> Items => items;

    public bool AddItem(DraggableItem item)
    {
        if(maxCount > 0)
            if (items.Count >= maxCount)
                return false;

        item.transform.SetParent(holder);
        return true;
    }

    public void RemoveItem(DraggableItem item)
    {
        if (items.Contains(item))
            items.Remove(item);
    }
}

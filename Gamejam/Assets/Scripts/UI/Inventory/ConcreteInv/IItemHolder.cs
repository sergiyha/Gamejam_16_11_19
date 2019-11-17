using System.Collections.Generic;

namespace UI.Inventory
{
    interface IItemHolder
    {
        DraggableItem Item { get; }

        IEnumerable<DraggableItem> Items { get; }
        
        bool AddItem(DraggableItem item);

        void RemoveItem(DraggableItem item);
    }
}

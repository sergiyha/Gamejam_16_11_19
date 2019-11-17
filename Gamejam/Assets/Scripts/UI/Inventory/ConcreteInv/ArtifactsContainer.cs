using System;
using System.Collections.Generic;
using UI.Inventory;

namespace Assets.Scripts.UI.Inventory.ConcreteInv
{
    public class ArtifactsContainer : Container
    {
        public event Action<ArtifactItem> OnAddArtifact = delegate { };
        
        public override bool AddItem(DraggableItem item)
        {
            ((ArtifactItem) item).IsLocked = true;
            OnAddArtifact((ArtifactItem) item);
            return base.AddItem(item);
        }

        public void Set(List<ArtifactBase> artifacts)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Destroy(items[i].gameObject);
            }

            items.Clear();

            foreach (var artifact in artifacts)
            {
                var item = Instantiate(prefab.gameObject, holder).GetComponent<ArtifactItem>();
                item.Init(artifact, dragContainer);
                items.Add(item);
            }
        }
    }
}

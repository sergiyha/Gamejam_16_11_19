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
            if (((ArtifactItem) item).Item is UsableArtifact)
            {
                ((ArtifactItem) item).IsLocked = true;
                OnAddArtifact((ArtifactItem) item);
                return base.AddItem(item);
            }

            return false;
        }

        public void Set(List<UsableArtifact> artifacts)
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
                item.IsLocked = true;
                items.Add(item);
            }
        }
    }
}

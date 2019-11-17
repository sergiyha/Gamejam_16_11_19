using UnityEngine;

namespace UI.Inventory
{
    public class SquadInventoryUI : Container
    {
        [SerializeField]
        private ArtifactItem prefab;

        [SerializeField]
        private Transform dragContainer;

        private void OnEnable()
        {
            foreach (var artifact in SquadInventory.Instance.artifacts)
            {
                var item = Instantiate(prefab.gameObject, holder).GetComponent<ArtifactItem>();
                item.Init(artifact, dragContainer);
            }
            SquadInventory.Instance.OnItemAdded += InstanceOnOnItemAdded;
        }

        private void InstanceOnOnItemAdded(ArtifactBase obj)
        {
            var newItem = Instantiate(prefab, holder).GetComponent<ArtifactItem>();
            newItem.Init(obj, dragContainer);
            base.AddItem(newItem);
        }

        private void OnDisable()
        {
            for (int i = 0; i < holder.childCount; i++)
            {
                Destroy(holder.GetChild(i).gameObject);
            }

            if (SquadInventory.Instance != null)
                SquadInventory.Instance.OnItemAdded -= InstanceOnOnItemAdded;
        }

        public override bool AddItem(DraggableItem item)
        {
            var added = base.AddItem(item);

            if (added)
            {
                var artifactItem = (ArtifactItem) item;
                SquadInventory.Instance.AddArtefact(artifactItem.Item);
            }

            return added;
        }

        public override void RemoveItem(DraggableItem item)
        {
            var artifactItem = (ArtifactItem)item;
            SquadInventory.Instance.RemoveArtefact(artifactItem.Item);

            base.RemoveItem(item);
        }
    }
}

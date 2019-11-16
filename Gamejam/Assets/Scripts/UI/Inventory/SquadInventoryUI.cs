using UnityEngine;
using System;

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
        }

        private void OnDisable()
        {
            for (int i = 0; i < holder.childCount; i++)
            {
                Destroy(holder.GetChild(i).gameObject);
            }
        }
    }
}

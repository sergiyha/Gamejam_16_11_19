using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class ArtifactItem : DraggableItem
    {
        public ArtifactBase Item { get; protected set; }

        public Image icon;

        public void Init(ArtifactBase artifactBase, Transform containerToDrag)
        {
            Item = artifactBase;
            _contriner = containerToDrag;
            icon.sprite = artifactBase.InventoryIcon;
        }
    }
}

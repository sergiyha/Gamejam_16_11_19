using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class ArtifactItem : DraggableItem
    {
        public Image icon;

        public void Init(ArtifactBase artifactBase, Transform containerToDrag)
        {
            _contriner = containerToDrag;
            icon.sprite = artifactBase.InventoryIcon;
        }
    }
}

using System;
using Assets.Scripts.UI.Inventory.ConcreteInv;
using UI.Inventory;
using UI.Inventory.ConcreteInv;
using UnityEngine;
using UnityEngine.UI;

namespace UI.DetailPanel
{
    public class DetailPanel : MonoBehaviour
    {
        [SerializeField]
        private Transform portraits;

        [SerializeField]
        private Image icon;
        [SerializeField]
        private WeaponSlot weaponSlot;
        [SerializeField]
        private ArtifactsContainer artifactsContainer;

        [SerializeField]
        private GameObject artifactItemPrefab;

        private Outline lastButton;
        private Character currentCharacter;

        private void Awake()
        {
            weaponSlot.OnAddItem += WeaponSlotOnOnAddItem;
            weaponSlot.OnRemoveItem += WeaponSlotOnOnRemoveItem;

            artifactsContainer.OnAddArtifact += ArtifactsContainerOnOnAddArtifact;
        }

        private void ArtifactsContainerOnOnAddArtifact(ArtifactItem obj)
        {
            currentCharacter.ArctifactsController.AddArtifact((UsableArtifact) obj.Item);
        }

        private void WeaponSlotOnOnRemoveItem(ArtifactItem obj)
        {
            SquadInventory.Instance.AddArtefact(obj.Item);
        }

        private void WeaponSlotOnOnAddItem(ArtifactItem obj)
        {
            currentCharacter.WeaponController.AddWeapon((WeaponScriptableObject) obj.Item);
        }

        // Start is called before the first frame update
        private void OnEnable()
        {
            var characters = Character.Characters[Character.CharType.Player];

            int index = 0;
            foreach (Character character in characters)
            {
                var newPortrait = Instantiate(portraits.GetChild(0), portraits);
                newPortrait.gameObject.SetActive(true);

                newPortrait.GetChild(1).GetChild(0).GetComponent<Image>().sprite = character.Icon;

                var curChar = character;
                newPortrait.GetComponent<Button>().onClick.AddListener(() => SelectChar(curChar, newPortrait.gameObject));
                newPortrait.GetComponentInChildren<Outline>().enabled = false;


                if (index == 0)
                {
                    SelectChar(curChar, newPortrait.gameObject);
                }

                index++;
            }
        }

        private void SelectChar(Character character, GameObject button)
        {
            if (lastButton != null)
                lastButton.enabled = false;

            lastButton = button.GetComponentInChildren<Outline>();
            lastButton.enabled = true;

            currentCharacter = character;

            icon.sprite = character.Icon;

            var weaponItem = Instantiate(artifactItemPrefab, weaponSlot.transform).GetComponent<ArtifactItem>();
            weaponItem.Init(character.WeaponController.Weapon, transform);
            weaponItem.transform.localScale = Vector3.one;

            weaponSlot.ReplaceItem(weaponItem);
            artifactsContainer.Set(character.ArctifactsController.Artifacts);
        }

        private void OnDisable()
        {
            var childCount = portraits.childCount;
            for (int i = 1; i < childCount; i++)
            {
                Destroy(portraits.GetChild(i).gameObject);
            }
        }
    }
}

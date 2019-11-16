using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerHudController : MonoBehaviour
    {
        private static PlayerHudController instance;

        public static PlayerHudController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PlayerHudController>();
                }

                return instance;
            }
        }

        [SerializeField]
        private PlayerPortrait playerPortraitPrefab;

        [SerializeField]
        private Transform portraitsHolder;

        private void Awake()
        {
            instance = this;
        }

        public (Slider, Transform, Transform) GetCharacterPanel(Sprite icon)
        {
            var portrait = Instantiate(playerPortraitPrefab, portraitsHolder).GetComponent<PlayerPortrait>();
            portrait.icon.sprite = icon;

            return (portrait.helthBar, portrait.helthGrid, portrait.effectsGrid);
        }
    }
}

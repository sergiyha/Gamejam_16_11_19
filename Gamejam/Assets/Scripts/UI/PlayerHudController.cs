using System;
using FPSTestProject.Helpers.Runtime.SoundManager;
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

        [SerializeField]
        private GameObject pauseContainer;

        private void Awake()
        {
            instance = this;
        }

        public (Slider, Transform, Transform) GetCharacterPanel(Character character)
        {
            var portrait = Instantiate(playerPortraitPrefab, portraitsHolder).GetComponent<PlayerPortrait>();
            portrait.gameObject.SetActive(true);
            portrait.icon.sprite = character.Icon;

            character.OnDead += (chara) => { Destroy(portrait.gameObject); };

            return (portrait.helthBar, portrait.helthGrid, portrait.effectsGrid);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale > 0)
                {
                    Time.timeScale = 0;
                    pauseContainer.SetActive(true);
                    SoundManager.Instance.PlayUI(SoundManagerDatabase.GetRandomClip(SoundType.UIOpen));
                }
                else
                {
                    Time.timeScale = 1;
                    pauseContainer.SetActive(false);
                    SoundManager.Instance.PlayUI(SoundManagerDatabase.GetRandomClip(SoundType.UIClose));
                }
            }
        }
    }
}

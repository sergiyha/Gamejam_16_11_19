using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Characters.Controllers
{
    public class UiController : MonoBehaviour
    {
        [SerializeField]
        private Character character;


        [SerializeField]
        private Slider healthSlider;
        [SerializeField]
        private Transform healthGrid;

        [SerializeField]
        private Transform effectsContainer;

        public void Awake()
        {
            if (character.CharacterType == Character.CharType.Player)
            {
                var data = PlayerHudController.Instance.GetCharacterPanel(character.Icon);
                healthSlider = data.Item1;
                healthGrid = data.Item2;

                effectsContainer = data.Item3;
            }
        }

        public void UpdateHealth(int characterHealth, int characterInitialHealth)
        {
            healthSlider.value = Mathf.Clamp01(characterHealth * 1.0f / characterInitialHealth);

            var gridChildCount = healthGrid.childCount;
            var needGridCount = Mathf.FloorToInt(characterInitialHealth / 5f);
            if (gridChildCount > needGridCount)
            {
                int needToRemove = gridChildCount - needGridCount;
                foreach (Transform child in healthGrid)
                {
                    if (needToRemove <= 0)
                        break;
                    needToRemove--;
                    Destroy(child.gameObject);
                }
            }
            else if (gridChildCount < needGridCount)
            {
                var prototype = healthGrid.GetChild(0);
                for (int i = 0; i < needGridCount - gridChildCount; i++)
                {
                    Instantiate(prototype.gameObject, healthGrid);
                }
            }
        }

        public void AddStatusEffect(StatusEffectBase statusEffect)
        {
            var prototype = effectsContainer.GetChild(0);
            var effectHolder = Instantiate(prototype.gameObject, effectsContainer);
            effectHolder.GetComponent<StatusEffectUI>().SetUp(statusEffect, false);
            effectHolder.name = statusEffect.InstanceId.ToString();
            effectHolder.SetActive(true);
        }

        public void RemoveStatusEffect(StatusEffectBase statusEffect)
        {
            var childCount = effectsContainer.childCount;
            for (int i = 0; i < childCount; i++)
            {
                if (effectsContainer.GetChild(i).name.Equals(statusEffect.InstanceId.ToString()))
                {
                    Destroy(effectsContainer.GetChild(i));
                    return;
                }
            }
        }
    }
}

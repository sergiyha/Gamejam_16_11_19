using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectUI : MonoBehaviour
{
    public Image Image;
    public TextMeshProUGUI tooltipText;
    public TextMeshProUGUI actriveTime;
    public TextMeshProUGUI cooldown;

    private StatusEffectBase current;
    private bool update;

    public void SetUp(StatusEffectBase effect, bool liveUpdate = false)
    {
        current = effect;

        UpdateUI();
        Image.sprite = effect.data.statusEffectIcon;
        tooltipText.text = effect.data.name;
    }

    private void Update()
    {
        if(!update)
            return;

        UpdateUI();
    }

    private void UpdateUI()
    {
    }
}

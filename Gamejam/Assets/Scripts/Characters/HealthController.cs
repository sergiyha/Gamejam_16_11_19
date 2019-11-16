using System;
using System.Collections;
using System.Collections.Generic;
using LifelongAdventure.Creatures.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Transform grid;

    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Start()
    {
        UpdateHealth();
    }

    public void DoDamage(int value)
    {
        character.Stats[Stat.Health] -= value;
        UpdateHealth();
    }

    public void DoHeal(int value)
    {
        character.Stats[Stat.Health] += value;
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        healthSlider.value = Mathf.Clamp01(character.Stats[Stat.Health] * 1.0f / character.InitialStats[Stat.Health]);

        var gridChildCount = grid.childCount;
        var needGridCount = Mathf.FloorToInt(character.InitialStats[Stat.Health] / 5f);
        if (gridChildCount > needGridCount)
        {
            int needToRemove = gridChildCount - needGridCount;
            foreach (Transform child in grid)
            {
                if (needToRemove <= 0)
                    break;
                needToRemove--;
                Destroy(child.gameObject);
            }
        }
        else if (gridChildCount < needGridCount)
        {
            var prototype = grid.GetChild(0);
            for (int i = 0; i < needGridCount - gridChildCount; i++)
            {
                Instantiate(prototype.gameObject, grid);
            }
        }
    }

    [Button]
    public void Test()
    {
        DoDamage(3);
    }
}

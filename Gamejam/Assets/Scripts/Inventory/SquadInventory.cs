using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadInventory : Inventory
{
    public static SquadInventory Instance;

    private void Awake()
    {
        Instance = this;
    }
}

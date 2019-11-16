﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int capasity;
    public List<ArtifactBase> artifacts;
    //public List<GameObject> UIobjs;

    public void AddArtefact(ArtifactBase art)
    {
        if (artifacts.Count < capasity)
        {
            artifacts.Add(art);
        }
    }
}

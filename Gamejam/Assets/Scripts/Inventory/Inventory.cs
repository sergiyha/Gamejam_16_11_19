using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int capasity;

    public List<ArtifactBase> artifacts;

    public event Action<ArtifactBase> OnItemAdded = delegate { };

    public void AddArtefact(ArtifactBase art)
    {
        if (artifacts.Count < capasity)
        {
            artifacts.Add(art);
            OnItemAdded(art);
        }
    }

    public void RemoveArtefact(ArtifactBase art)
    {
        if (artifacts.Contains(art))
        {
            artifacts.Remove(art);
        }
    }
}

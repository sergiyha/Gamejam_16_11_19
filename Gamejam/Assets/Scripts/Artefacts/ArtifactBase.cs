using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactBase : ScriptableObject 
{
    public Sprite SlotIcon;
    public string Name;
    public string Description;
    public Sprite inventoryIcon;
    protected List<Character> targets;
    
    public virtual void Action()
    {
        
    }

    public void SetTargets(List<Character> targ)
    {
        targets = targ;
    }

    public void DisableArtifact()
    {
        
    }

    public void EnableArtifact()
    {
    }
    
}

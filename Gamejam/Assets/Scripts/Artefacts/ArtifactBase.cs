using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactBase : ScriptableObject 
{
    public Sprite SlotIcon;
    public string Name;
    public string Description;
    protected Character[] targets;
    
    public virtual void Action()
    {
        
    }

    public void SetTargets(Character[] targ)
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

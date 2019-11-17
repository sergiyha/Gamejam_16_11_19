using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactBase : ScriptableObject 
{
    public Sprite SlotIcon;
    public string Name;
    public string Description;
    public Sprite StatusEffectIcon;
    public Sprite InventoryIcon;
    public ItemType ArtefactType;
    protected List<Character> targets = new List<Character>();
    
    public virtual void Action()
    {
        
    }

    public void SetTargets(List<Character> targ)
    {
        targets = targ;
    }

    public virtual void  DisableArtifact()
    {
        
    }

    public virtual void EnableArtifact()
    {
    }
    
}

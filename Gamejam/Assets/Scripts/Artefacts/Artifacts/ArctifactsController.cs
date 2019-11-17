using System.Collections;
using System.Collections.Generic;
using FPSTestProject.Helpers.Runtime.SoundManager;
using UnityEngine;

public class ArctifactsController : MonoBehaviour
{
    public int capacity;
    public List<UsableArtifact> Artifacts;
    public List<float> currentCooldowns;
    
    public float atackspeedMultipl =1f;//==1f if normal
    //public bool UseAllowed;
    
    private Character character;

    private List<List<Character>> allArtifactsTarg;
    

    private void Awake()
    {
        allArtifactsTarg = new List<List<Character>>();
        character = GetComponent<Character>();
        StartCoroutine(Use());
      
    }

    
    public void AddArtifact(UsableArtifact artifact)
    {
        Artifacts.Add(artifact);
        var tmp = new List<Character>();
        allArtifactsTarg.Add(tmp);
        currentCooldowns.Add(0.5f);
    }

    public List<Character> LookForTargets(UsableArtifact artifact, int index)
    {
        //allArtifactsTarg[index].Clear();
        allArtifactsTarg[index] = new List<Character>();
        if (artifact.TargetType == TargetType.ally)
        {
            foreach (var characters in Character.Characters)
            {
                if (characters.Key == character.CharacterType)
                    allArtifactsTarg[index].AddRange(characters.Value);
            }
        }
        if (artifact.TargetType == TargetType.enemy)
        {
            foreach (var characters in Character.Characters)
            {
                if (characters.Key != character.CharacterType)
                    allArtifactsTarg[index].AddRange(characters.Value);
            }
        }
        if (artifact.TargetType == TargetType.itself)
        {
            allArtifactsTarg[index].Add(character);
        }

        for (int i = 0; i < allArtifactsTarg[index].Count; i++)
        {
            if (Vector3.Distance(allArtifactsTarg[index][i].transform.position, transform.position) > artifact.Range
                || Vector3.Angle(transform.forward, (allArtifactsTarg[index][i].transform.position - transform.position).normalized) <= artifact.Angle)
            {
                allArtifactsTarg[index].RemoveAt(i);
                i--;
            }
        }

        return allArtifactsTarg[index];
    }

    public IEnumerator Use()
    {
        while (true)
        {
            for (int i=0; i<Artifacts.Count; i++)
            {
                //Debug.Log(Artifacts[i].name + " " +Artifacts[i].currentCooldown );
                if (currentCooldowns[i] > 0f)
                {
                
                    currentCooldowns[i] -= Time.deltaTime;
                    //Debug.Log("Cd");
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    LookForTargets(Artifacts[i],i);
                    Artifacts[i].SetTargets(allArtifactsTarg[i]);
                    Artifacts[i].ready = true;
                    if (Artifacts[i].CanPerform())
                    {
                        
                        Artifacts[i].Action();
                        SoundManager.Instance.PlaySFX(SoundManagerDatabase.GetRandomClip(Artifacts[i].ActionSound), transform.position,2f);
                        currentCooldowns[i] = Artifacts[i].Cooldown/atackspeedMultipl;
                        Artifacts[i].ready = false;
                        yield return new WaitForEndOfFrame();
                    }
                    else
                    {
                        yield return new WaitForEndOfFrame();
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        
        }
        yield return null;
    }
}

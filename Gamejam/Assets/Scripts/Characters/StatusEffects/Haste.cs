using LifelongAdventure.Creatures.Data;
using UnityEngine;

namespace Characters.StatusEffects
{
    public class Haste : StatusEffectBase
    {
        private HasteEffectData Data;
        public override void ApplyEffect()
        {
            Debug.Log("haste");
            character.Stats[Stat.MoveSpeed] = (int)(character.Stats[Stat.MoveSpeed] * Data.speedMultiplier);
            character.StatusEffectsController.hasteFX.SetActive(true);
        }

        public override void Remove()
        {
            character.Stats[Stat.MoveSpeed] = (int)(character.Stats[Stat.MoveSpeed] / Data.speedMultiplier);
            character.StatusEffectsController.hasteFX.SetActive(false);
        }

        public Haste(BaseEffectData data, Character character) : base(data, character)
        {
            Data = (HasteEffectData) data;
            TotalTime = Data.totalTime;
            tickTime = Data.tickTime;
            this.character = character;
            
        }
    }
}

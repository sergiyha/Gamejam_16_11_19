using LifelongAdventure.Creatures.Data;

namespace Characters.StatusEffects
{
    public class Slow : StatusEffectBase
    { 
//        public float tickTime; //if tickTime =0 then sigle use while active
//        public float currentTickLeft;
//        public float TotalTime;//if 1000000 then aura
//        public float currentTotalTime = 0f;
//        public bool active;
//    
//        public Character character;
        private SlowEffectData Data;
        public override void ApplyEffect()
        {
            //Debug.Log("haste");
            character.Stats[Stat.MoveSpeed] = (int)(character.Stats[Stat.MoveSpeed] * Data.speedMultiplier);
            character.StatusEffectsController.hasteFX.SetActive(true);
        }

        public override void Remove()
        {
            character.Stats[Stat.MoveSpeed] = (int)(character.Stats[Stat.MoveSpeed] / Data.speedMultiplier);
            character.StatusEffectsController.hasteFX.SetActive(false);
        }

        public Slow(BaseEffectData data, Character character) : base(data, character)
        {
            Data = (SlowEffectData) data;
            TotalTime = Data.totalTime;
            tickTime = Data.tickTime;
            this.character = character;
            
        }
    }
}

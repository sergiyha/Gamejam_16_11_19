namespace Characters.StatusEffects
{
    public class Bleed : StatusEffectBase
    { 
//        public float tickTime; //if tickTime =0 then sigle use while active
//        public float currentTickLeft;
//        public float TotalTime;//if 1000000 then aura
//        public float currentTotalTime = 0f;
//        public bool active;
//    
//        public Character character;
        private BleedEffectData Data;
        public Bleed(BaseEffectData data, Character character) : base(data,character)
        {
            Data = (BleedEffectData) data;

            TotalTime = Data.totalTime;
            tickTime = Data.tickTime;
            this.character = character;
        }
        
        public override void ApplyEffect()
        {
            character.HealthController.DoDamage(Data.damagePerTick, -1);
        }
    }
}

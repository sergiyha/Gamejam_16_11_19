namespace Characters.StatusEffects
{
    public class Rage : StatusEffectBase
    {
        private RageEffectData Data;
        

        

        public override void Remove()
        {
            character.WeaponController.atackspeedMultipl  -= Data.additionaAS;
        }

        public Rage(BaseEffectData data, Character character) : base(data, character)
        {
            Data = (RageEffectData) data;
            TotalTime = Data.totalTime;
            tickTime = Data.tickTime;
            this.character = character;
        }
        
        public override void ApplyEffect()
        {
            character.WeaponController.atackspeedMultipl  += Data.additionaAS;
        }
    }
}

using LifelongAdventure.Creatures.Data;

namespace Characters.StatusEffects
{
    public class Haste : StatusEffectBase
    {
        private HasteEffectData Data;
        public override void ApplyEffect()
        {
            character.Stats[Stat.MoveSpeed] = (int)(character.Stats[Stat.MoveSpeed] * Data.speedMultiplier);
        }

        public override void Remove()
        {
            character.Stats[Stat.MoveSpeed] = (int)(character.Stats[Stat.MoveSpeed] / Data.speedMultiplier);
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

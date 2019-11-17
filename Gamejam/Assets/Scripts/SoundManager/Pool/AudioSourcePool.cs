using FPSTestProject.Helpers.Runtime.ObjectPool;

namespace FPSTestProject.Helpers.Runtime.SoundManager.Pool
{
    public class AudioSourcePool : ObjectPool<PooledAudioSource>
    {
        protected override bool KeepBetweenLevels => true;

        protected override int InitialCount => 50;

        protected override string Prefab => "SoundManager/AudioSource";
    }
}

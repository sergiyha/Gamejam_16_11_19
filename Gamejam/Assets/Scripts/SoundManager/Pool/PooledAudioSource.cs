using FPSTestProject.Helpers.Runtime.ObjectPool;
using UnityEngine;
using UnityEngine.Audio;

namespace FPSTestProject.Helpers.Runtime.SoundManager.Pool
{
    public class PooledAudioSource : MonoBehaviour, IPoolable
    {
        [SerializeField]
        private AudioSource audioSource;

        public bool IsActive => gameObject.activeSelf;

        private Transform originalParent;

        public void Activate(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;

            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            if (originalParent != null)
                transform.SetParent(originalParent);

            originalParent = null;
            gameObject.SetActive(false);
        }

        public void Play(bool spaceSound, AudioClip clip, AudioMixerGroup group, bool loop, float randomRange, bool fadeIn, float addVolume = 1, Transform attachTo = null, bool linear = false)
        {
            audioSource.clip = clip;
            audioSource.outputAudioMixerGroup = group;
            audioSource.loop = loop;
            audioSource.volume = addVolume;
            audioSource.spatialBlend = spaceSound ? 1 : 0;
            audioSource.pitch = 1 + Random.Range(-randomRange, randomRange);
            audioSource.Play();
            audioSource.rolloffMode = linear ? AudioRolloffMode.Linear : AudioRolloffMode.Logarithmic;

            if (!audioSource.loop)
                Invoke(nameof(Deactivate), clip.length * audioSource.pitch);

            if (attachTo != null)
            {
                originalParent = transform.parent;
                transform.SetParent(attachTo);
            }
        }
    }
}

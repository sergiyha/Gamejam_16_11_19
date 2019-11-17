using System;
using System.Collections.Generic;
using System.IO;
using FPSTestProject.Helpers.Runtime.ObjectPool;
using FPSTestProject.Helpers.Runtime.SoundManager.Pool;
using UnityEngine;
using UnityEngine.Audio;

namespace FPSTestProject.Helpers.Runtime.SoundManager
{
    public class SoundManager : MonoBehaviour
    {
        private const string settingsFile = "SoundManagerSettings.json";

        private static SoundManager instance;

        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Instantiate(Resources.Load<GameObject>("SoundManager/SoundManager"))
                        .GetComponent<SoundManager>();
                    instance.Initialize();
                }

                return instance;
            }
        }

        #region Serializable

        [SerializeField]
        private AudioMixerGroup global;
        [SerializeField]
        private AudioMixerGroup musicGroup;
        [SerializeField]
        private AudioMixerGroup uiGroup;
        [SerializeField]
        private AudioMixerGroup sfxGroup;

        #endregion

        public float GlobalVolume
        {
            get => currentSettings.global;
            set
            {
                currentSettings.global = Mathf.Clamp01(value);
                SetUpSettings();
            }
        }
        public float MusicVolume
        {
            get => currentSettings.musicVolume;
            set
            {
                currentSettings.musicVolume = Mathf.Clamp01(value);
                SetUpSettings();
            }
        }
        public float SfxVolume
        {
            get => currentSettings.sfxVolume;
            set
            {
                currentSettings.sfxVolume = Mathf.Clamp01(value);
                SetUpSettings();
            }
        }
        public float UiVolume
        {
            get => currentSettings.uiVolume;
            set
            {
                currentSettings.uiVolume = Mathf.Clamp01(value);
                SetUpSettings();
            }
        }

        private bool isInitialized;

        private ObjectPool<PooledAudioSource> audioSourcePool;

        private Settings currentSettings;

        private List<PooledAudioSource> music = new List<PooledAudioSource>();

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Initialize();
            DontDestroyOnLoad(gameObject);
        }

        private void Initialize()
        {
            if(isInitialized)
                return;

            audioSourcePool = PoolInitializer.GetPool<PooledAudioSource>();
            LoadSettings();

            isInitialized = true;
        }

        public void PlayMusic(AudioClip clip, float addVolume = 1)
        {
            var audioSource = audioSourcePool.GetObject(Vector3.zero, Quaternion.identity);
            music.Add(audioSource);
            audioSource.Play(false, clip, musicGroup, true, 0, true, addVolume);
        }

        public void PlayUI(AudioClip clip, float addVolume = 1)
        {
            audioSourcePool.GetObject(Vector3.zero, Quaternion.identity).Play(false, clip, uiGroup, false, 0, false, addVolume);
        }

        public void PlaySFX(AudioClip clip, Vector3 position, float addVolume = 1, Transform attachTo = null, bool linear = false, float random = 0)
        {
            audioSourcePool.GetObject(position, Quaternion.identity)
                .Play(true, clip, sfxGroup, false, random, false, addVolume, attachTo, linear);
        }

        public void StopAllMusic()
        {
            foreach (PooledAudioSource source in music)
            {
                source.Deactivate();
            }

            music.Clear();
        }

        #region Settings

        public void SaveSettings()
        {
            File.WriteAllText($"{Application.persistentDataPath}/{settingsFile}", JsonUtility.ToJson(currentSettings));
        }

        private void LoadSettings()
        {
            if (!File.Exists($"{Application.persistentDataPath}/{settingsFile}"))
            {
                currentSettings = new Settings()
                {
                    musicVolume = 1,
                    sfxVolume = 1,
                    uiVolume = 1,
                    global = 0.75f
                };
                SaveSettings();
                SetUpSettings();
                return;
            }

            currentSettings =
                JsonUtility.FromJson<Settings>(File.ReadAllText($"{Application.persistentDataPath}/{settingsFile}"));
            SetUpSettings();
        }

        private void SetUpSettings()
        {
            global.audioMixer.SetFloat("GlobalVolume", currentSettings.global <= 0.01f ? -80f : Mathf.Log(currentSettings.global) * 20);
            musicGroup.audioMixer.SetFloat("MusicVolume", currentSettings.musicVolume <= 0.01f ? -80f : Mathf.Log(currentSettings.musicVolume) * 20);
            uiGroup.audioMixer.SetFloat("UiVolume", currentSettings.uiVolume <= 0.01f ? -80f : Mathf.Log(currentSettings.uiVolume) * 20);
            sfxGroup.audioMixer.SetFloat("SfxVolume", currentSettings.sfxVolume <= 0.01f ? -80f : Mathf.Log(currentSettings.sfxVolume) * 20);
        }

        [Serializable]
        private struct Settings
        {
            public float global;
            public float musicVolume;
            public float uiVolume;
            public float sfxVolume;
        }

        #endregion
    }
}

using UnityEngine;

namespace CracklingLeafTools.AudioSystem.Core
{
    [DefaultExecutionOrder(-50)]    //ensures this initializes early
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] UnityEngine.Audio.AudioMixer audioMixer;
        [SerializeField] AudioPool audioPool;
        [SerializeField] SoundLibrary[] soundLibraries;

        #region Setup
        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);  //persistent
            Initialize();
        }
        void Initialize()
        {
            audioPool.Initialize();
            //set up pool, load definitions, prepare mixer groups
        }

        #endregion

        #region Public Playback API
        /// <summary>
        /// Play sound using the sound definition
        /// </summary>
        /// <param name="def"></param>
        public void PlaySound(SoundDefinition def)
        {
            Debug.Log($"[audio manager] Play Sound: {def}");
            if(def == null || def.clip == null) { return; }

            AudioSource src = audioPool.GetAvailableSource();
            if(src == null ) { return; }

            Debug.Log($"source found");

            src.clip = def.clip;
            src.volume = def.volume;
            src.pitch = (def.randomPitchMin != def.randomPitchMax)
                ? Random.Range(def.randomPitchMin, def.randomPitchMax)
                : def.pitch;

            src.loop = def.loop;

            if(def.outputGroup != null)
            {
                src.outputAudioMixerGroup = def.outputGroup;
            }
            Debug.Log($"play");
            src.Play();
            audioPool.StartWatcher(src);
        }

        public void PlaySound(string guid)
        {
            foreach(SoundLibrary lib in soundLibraries)
            {
                SoundDefinition def = lib.Get(guid);
                if(def != null)
                {
                    PlaySound(def);
                    return;
                }
            }
        }
        /// <summary>
        /// Stops all currently playing sounds. Optional use cases.
        /// </summary>
        public void StopAllSounds()
        {
            if(audioPool != null) { audioPool.StopAll(); }
        }

        #endregion

        #region Volume and Mixer Controls
        /// <summary>
        /// Sets the volume for a specific audio channel.
        /// </summary>
        public void SetVolume(string channelName, float volume)
        {
            //todo: update internal state or audio mixer parameter.
        }
        #endregion

        #region Debug and Editor Helpers
        private void OnValidate()
        {
            //optional: auto assign mixer or validate references in editor.
        }
        #endregion

    }
}


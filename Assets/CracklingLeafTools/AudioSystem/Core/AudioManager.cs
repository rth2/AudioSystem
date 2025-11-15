using UnityEngine;

namespace CracklingLeafTools.AudioSystem.Core
{
    [DefaultExecutionOrder(-50)]    //ensures this initializes early
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] UnityEngine.Audio.AudioMixer audioMixer;
        [SerializeField] AudioPool audioPool;

        #region Setup
        private void Awake()
        {
            Initialize();
        }
        void Initialize()
        {
            Debug.Log($"[Initialize] TODO");
            audioPool.Initialize();
            //set up pool, load definitions, prepare mixer groups
        }

        #endregion

        #region Public Playback API
        /// <summary>
        /// Requests a sound to play by name or key.
        /// </summary>
        /// <param name="soundKey">The identifier for the sound to play.</param>
        public void PlaySound(string soundKey)
        {
            //TODO: Lookup sound definition and trigger audiosource
            AudioSource source = audioPool.GetAvailableSource();
        }
        /// <summary>
        /// Stops all currently playing sounds. Optional use cases.
        /// </summary>
        public void StopAllSounds()
        {
            //todo: tell pool to stop or fade out all active audiosources
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


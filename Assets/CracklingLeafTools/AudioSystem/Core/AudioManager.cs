using System;
using System.Collections.Generic;
using System.Linq;
using Codice.CM.Common.Zlib;
using CracklingLeafTools.AudioSystem.Core.Editor;
using UnityEngine;

namespace CracklingLeafTools.AudioSystem.Core
{
    [DefaultExecutionOrder(-50)]    //ensures this initializes early
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] UnityEngine.Audio.AudioMixer audioMixer;
        [SerializeField] AudioPool audioPool;
        [SerializeField] SoundLibrary[] persistentLibraries;
        List<SoundLibrary> sceneLibraries;

        bool isInitialized = false;

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
            if (isInitialized) { return; }
            isInitialized = true;
            sceneLibraries = new();
            audioPool.Initialize();
            //set up pool, load definitions, prepare mixer groups
        }

        #endregion

        #region Library Management
        public void ClearSceneLibraries() { sceneLibraries.Clear(); }
        public void AddSceneLibrary(SoundLibrary library)
        {
            if(library != null && !persistentLibraries.Contains(library) && !sceneLibraries.Contains(library)) { sceneLibraries.Add(library); }
        }
        public void RemoveSceneLibrary(SoundLibrary library)
        {
            sceneLibraries.Remove(library);
        }
        #endregion

        #region Public Playback API
        /// <summary>
        /// Play sound using the sound definition
        /// </summary>
        /// <param name="def"></param>
        public void PlaySound(SoundDefinition def)
        {
            if(def == null || def.clip == null) { return; }

            AudioSource src = audioPool.GetAvailableSource();
            if(src == null ) { return; }

            src.clip = def.clip;
            src.volume = def.volume;
            src.pitch = (def.randomPitchMin != def.randomPitchMax)
                ? UnityEngine.Random.Range(def.randomPitchMin, def.randomPitchMax)
                : def.pitch;

            src.loop = def.loop;

            if(def.outputGroup != null)
            {
                src.outputAudioMixerGroup = def.outputGroup;
            }
            src.Play();
            audioPool.StartWatcher(src);
        }

        public void PlaySound(string soundKey)
        {
            //parts[0] = lib name; parts[1] = soundDef name
            string[] parts = soundKey.Split(SoundKeyGenerator.Separator);
            for(int i = 0; i < parts.Length; i++) { parts[i] = SoundKeyGenerator.MakeStringSafe(parts[i]); }
            //Search scene libraries first
            foreach(SoundLibrary sceneLib in sceneLibraries)
            {
                if(sceneLib.SafeLibraryName == parts[0] && sceneLib.TryFind(parts[1], out SoundDefinition def))
                {
                    PlaySound(def);
                    return;
                }
            }
            //Search persistent libraries second
            foreach(SoundLibrary lib in persistentLibraries)
            {
                if (lib.SafeLibraryName == parts[0] && lib.TryFind(parts[1], out SoundDefinition sDef))
                {
                    PlaySound(sDef);
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

